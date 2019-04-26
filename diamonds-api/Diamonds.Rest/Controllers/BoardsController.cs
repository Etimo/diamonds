using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Diamonds.Common.Entities;
using Diamonds.Common.Storage;
using Diamonds.Common.Models;
using Diamonds.Common.GameEngine.Move;
using Diamonds.Common.GameEngine.GameObjects;
using System.Threading;
using Diamonds.Common.GameEngine.DiamondGenerator;

namespace Diamonds.Rest.Controllers
{
    [Route("api/[controller]")]
    public class BoardsController : Controller
    {
          IGameObjectGeneratorService _gameObjectGeneratorService;
        IStorage _storage;
        IMoveService _moveService;
        IDiamondGeneratorService _diamondGeneratorService;

        public BoardsController(IStorage storage, IMoveService moveService,
         IDiamondGeneratorService diamondGeneratorService,
         IGameObjectGeneratorService gameObjectGenerators)
        {
            this._storage = storage;
            this._moveService = moveService;
            this._diamondGeneratorService = diamondGeneratorService;
            this._gameObjectGeneratorService = gameObjectGenerators;
        }

        /// <summary>
        /// Get all boards
        /// </summary>
        [ProducesResponseType(typeof(IEnumerable<Board>), 200)]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var boards = await _storage.GetBoardsAsync();
            return Ok(boards);
        }

        private void regenerateBoardObjects(Board board){
                board.GameObjects = new List<BaseGameObject>();
                board.Diamonds = _diamondGeneratorService.GenerateDiamondsIfNeeded(board);
                if(_gameObjectGeneratorService==null)return;
                var list =
                 _gameObjectGeneratorService
                 .getCurrentObjectGenerators()
                 .SelectMany(
                     gog=>
                     gog.RegenerateObjects(board))
                     .ToList();
                 board.GameObjects = list;
        }

        /// <summary>
        /// Get board by id
        /// </summary>
        [ProducesResponseType(typeof(Board), 200)]
        [ProducesResponseType(typeof(void), 404)]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBoardAsync(string id)
        {
            var board = await GetAndGenerateBoardAsync(id);
            if (board == null)
            {
                return NotFound();
            }

            return Ok(board);
        }

        private async Task<Board> GetAndGenerateBoardAsync(string id)
        {
            var board = await _storage.GetBoardAsync(id);
            if(board == null)
            {
                return null;
            }

            if(_diamondGeneratorService.NeedToGenerateDiamonds(board))
            {
                regenerateBoardObjects(board);
                await _storage.UpdateBoardAsync(board);
            }
            return board;
        }

        /// <summary>
        /// Join board
        /// </summary>
        /// <response code="403">Bot doesn't exist</response>
        /// <response code="404">Board doesn't exist</response>
        /// <response code="409">Board is full or bot is already on board</response>
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(void), 403)]
        [ProducesResponseType(typeof(void), 404)]
        [ProducesResponseType(typeof(void), 409)]
        [HttpPost]
        [Route("{id}/join")]
        public async Task<IActionResult> PostAsync([FromBody] JoinInput input, string id)
        {
            // Check if bot exists
            var bot = await _storage.GetBotAsync(input.BotToken);
            if (bot == null)
            {
                // Invalid bot token
                return StatusCode(403);
            }

            // Check for correct board
            var board = await _storage.GetBoardAsync(id);
            if (board == null)
            {
                // Invalid board id
                return StatusCode(404);
            }

            // Check if board is full
            if (board.IsFull())
            {
                return StatusCode(409);
            }

            // Check if bot is already on board
            if (board.HasBot(bot))
            {
                return StatusCode(409);
            }

            board.AddBot(bot);
            await _storage.UpdateBoardAsync(board);

            SetExpireCallbackForBot(bot, id);

            return Ok();
        }

        private void SetExpireCallbackForBot(Bot bot, string id)
        {

            // intermediate solution to terminate bot when time expires
            // maybe replace with endpoint like /end or /score in the future
            var timerState = new TimerState { BotId = bot.Id, BoardId = id };
            var onExpireCallback = new TimerCallback(async x => await OnBotExpireAsync(x));

            var timer = new Timer(onExpireCallback, timerState, Board.TotalGameTime, Timeout.Infinite);
            timerState.TimerReference = timer;
        }

        private class TimerState
        {
            public string BotId { get; set; }
            public string BoardId { get; set; }
            public Timer TimerReference { get; set; }
        }

        private async Task OnBotExpireAsync(object stateObj)
        {
            try
            {
                var state = (TimerState)stateObj;
                var board = await _storage.GetBoardAsync(state.BoardId);

                var bot = board.Bots.SingleOrDefault(b => !string.IsNullOrWhiteSpace(b.BotId) && b.BotId.Equals(state.BotId));
                if (bot == null) return;

                board.Bots.Remove(bot);
                await _storage.UpdateBoardAsync(board);

                if (await ShouldSaveScoreAsync(bot.Name, bot.Score))
                {
                    var score = new Highscore
                    {
                        Id = Guid.NewGuid().ToString(),
                        BotName = bot.Name,
                        Score = bot.Score,
                        SessionFinishedAt = DateTime.UtcNow,
                    };

                    await _storage.SaveHighscoreAsync(score);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine($"{nameof(OnBotExpireAsync)} exception! {exc.ToString()}");
            }
        }

        private async Task<bool> ShouldSaveScoreAsync(string botName, int score)
        {
            var highestScore = (await _storage
                .GetHighscoresAsync(Common.Enums.SeasonSelector.Current, botName))
                .FirstOrDefault();

            return highestScore == null || score > highestScore.Score;
        }

        /// <summary>
        /// Move bot
        /// </summary>
        /// <response code="403">Bot doesn't exist, bot is not on board or trying to move too quickly after previous move (100ms must pass)</response>
        /// <response code="404">Board doesn't exist</response>
        /// <response code="409">Invalid movement</response>
        [ProducesResponseType(typeof(Board), 200)]
        [ProducesResponseType(typeof(void), 400)]
        [ProducesResponseType(typeof(void), 403)]
        [ProducesResponseType(typeof(void), 404)]
        [ProducesResponseType(typeof(Board), 409)]
        [HttpPost]
        [Route("{id}/move")]
        public async Task<IActionResult> PostAsync([FromBody] MoveInput input, string id)
        {
            if (input.isValid() == false)
            {
                return StatusCode(400);
            }

            var bot = await _storage.GetBotAsync(input.botToken);

            if (bot == null)
            {
                return StatusCode(403);
            }

            var board = await _storage.GetBoardAsync(id);

            if (board == null)
            {
                return StatusCode(404);
            }
            if (board.HasBot(bot) == false)
            {
                return StatusCode(403);
            }
            if (board.CanMove(bot) == false)
            {
                return StatusCode(403);
            }

            var moveResult = await _moveService.MoveAsync(id, bot.Name, input.direction);

            if (moveResult != MoveResultCode.Ok)
            {
                return StatusCode(409, GetAndGenerateBoardAsync(id));
            }

            return Ok(GetAndGenerateBoardAsync(id));
        }
    }
}
