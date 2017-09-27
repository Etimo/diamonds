using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Diamonds.Common.Storage;
using Diamonds.Common.Models;

namespace Diamonds.Rest.Controllers
{
    [Route("api/[controller]")]
    public class ExpiredBotsController : Controller
    {
        private IStorage _storage;

        public ExpiredBotsController(IStorage storage)
        {
            _storage = storage;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var boards = _storage.GetBoards().ToList();

            boards.ForEach(board =>
            {
                var expiredBots = board.Bots.Where(bot => bot.IsGameOver()).ToList();
                Console.WriteLine("expired: " + expiredBots.Count);

                expiredBots.ForEach(expiredBot =>
                {
                    Console.WriteLine("contains: " + board.Bots.Contains(expiredBot).ToString());
                    var removed = board.Bots.Remove(expiredBot);
                    Console.WriteLine("removed: " + removed.ToString());
                    _storage.UpdateBoard(board);
                });
            });
            return Ok();
        }
    }
}
