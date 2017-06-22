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

        [Route("{id}/join")]
        public IActionResult Post([FromHeader] JoinInput input, string id)
        {
            var auth = this.Request.Headers["Authorization"];
            var bot = _storage.GetBot(auth);
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
