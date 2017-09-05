using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Diamonds.Common.Entities;
using Diamonds.Common.Storage;
using Diamonds.Common.Models;
using Diamonds.Common.GameEngine.Move;
using System.Threading;
using Diamonds.Common.GameEngine.DiamondGenerator;

namespace Diamonds.Rest.Controllers
{
    [Route("[controller]")]
    public class BoardsController : Controller
    {
        IStorage _storage;
        IMoveService _moveService;
        IDiamondGeneratorService _diamondGeneratorService;

        public BoardsController(IStorage storage, IMoveService moveService, IDiamondGeneratorService diamondGeneratorService)
        {
            this._storage = storage;
            this._moveService = moveService;
            this._diamondGeneratorService = diamondGeneratorService;
        }

        public IActionResult Get()
        {
            var boards = _storage.GetBoards();
            return Ok(boards);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetBoard(string id)
        {

            var board = _storage.GetBoard(id);

            if (board == null)
            {
                return NotFound();
            }
            board.Diamonds = _diamondGeneratorService.GenerateDiamondsIfNeeded(board);
            _storage.UpdateBoard(board);

            return Ok(board);
        }

        [Route("{id}/join")]
        public IActionResult Post([FromBody] JoinInput input, string id)
        {
            // Check if bot exists
            var bot = _storage.GetBot(input.BotToken);
            if (bot == null)
            {
                // Invalid bot token
                return StatusCode(403);
            }

            // Check for correct board
            var board = _storage.GetBoard(id);
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
            _storage.UpdateBoard(board);

            SetExpireCallbackForBot(bot, id);

            return Ok();
        }
        private void SetExpireCallbackForBot(Bot bot, string id)
        {

            // intermediate solution to terminate bot when time expires
            // maybe replace with endpoint like /end or /score in the future
            var timerState = new TimerState { BotId = bot.Id, BoardId = id };
            var onExpireCallback = new TimerCallback(OnBotExpire);

            var timer = new Timer(onExpireCallback, timerState, Board.TotalGameTime, Timeout.Infinite);
            timerState.TimerReference = timer;
        }

        private class TimerState
        {
            public string BotId { get; set; }
            public string BoardId { get; set; }
            public Timer TimerReference { get; set; }
        }

        private void OnBotExpire(object stateObj)
        {
            var state = (TimerState)stateObj;
            var board = _storage.GetBoard(state.BoardId);

            var bot = board.Bots.SingleOrDefault(b => !string.IsNullOrWhiteSpace(b.BotId) && b.BotId.Equals(state.BotId));
            if (bot == null) return;

            board.Bots.Remove(bot);
            _storage.UpdateBoard(board);

            var score = new Highscore
            {
                Id = bot.Name,
                BotName = bot.Name,
                Score = bot.Score
            };

            _storage.SaveHighscore(score);
        }

        [Route("{id}/move")]
        public IActionResult Post([FromBody] MoveInput input, string id)
        {
            if (input.isValid() == false)
            {
                return StatusCode(400);
            }

            var bot = _storage.GetBot(input.botToken);

            if (bot == null)
            {
                return StatusCode(403);
            }

            var board = _storage.GetBoard(id);

            if (board == null)
            {
                return StatusCode(404);
            }
            if (board.HasBot(bot) == false)
            {
                return StatusCode(403);
            }

            var moveResult = _moveService.Move(id, bot.Name, input.direction);

            if (moveResult == MoveResultCode.CanNotMoveInThatDirection)
            {
                return StatusCode(409);
            }

            return GetBoard(id);
        }
    }
}
