using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Diamonds.Common.Entities;
using Diamonds.Common.Storage;
using Diamonds.Common.Models;

namespace Diamonds.Rest.Controllers
{
    [Route("[controller]")]
    public class BoardsController : Controller
    {
        IStorage _storage;

        public BoardsController(IStorage storage)
        {
            this._storage = storage;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var boards = _storage.GetBoards();
            return Ok(boards);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetBoard(string id) {

            var board = _storage.GetBoard(id);
            if (board == null) {
                return NotFound();
            }

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

            return Ok(new JoinOutput
            {
                BoardToken = bot.BoardToken
            }
            );
        }

        [HttpPost]
        [Route("{id}/move")]
        public IActionResult Move([FromBody] MoveInput input, string id)
        {
            var bot = _storage.GetBot(input.botToken);

            if (bot == null) {
                return StatusCode(403);
            }

            var board = _storage.GetBoard(id);

            if (board == null) {
                return StatusCode(404);
            }
            throw new NotImplementedException();
        }
    }
}
