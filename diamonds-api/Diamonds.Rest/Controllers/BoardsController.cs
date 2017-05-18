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
        IStorage storage;

        public BoardsController(IStorage storage)
        {
            this.storage = storage;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new List<Board>
                {
                    new Board
                    {
                        BoardId = "77", Height = 99, Width = 99,
                        Bots = new List<BoardBot>(),
                        Diamonds = new List<Position>()
                }
            });
        }

        [Route("{id}/join")]
        public IActionResult Post([FromHeader] JoinInput input, string id)
        {
            var auth = this.Request.Headers["Authorization"];
            var bot = storage.GetBot(auth);
            //X-Token
            // Authorza: bearer token
            // existing bot?
            // existing 
            // get board
            // bard.addbot(bot)
            // 
            return Ok();
        }
    }
}
