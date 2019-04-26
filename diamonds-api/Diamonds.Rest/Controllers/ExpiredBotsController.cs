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
        public async Task<IActionResult> GetAsync()
        {
            var boards = (await _storage.GetBoardsAsync()).ToList();

            boards.ForEach(board =>
            {
                var expiredBots = board.Bots.Where(bot => bot.IsGameOver()).ToList();

                expiredBots.ForEach(async expiredBot =>
                {
                    board.Bots.Remove(expiredBot);
                    await _storage.UpdateBoardAsync(board);
                });
            });
            return Ok();
        }
    }
}
