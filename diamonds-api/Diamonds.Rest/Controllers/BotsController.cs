using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Diamonds.Common.Storage;
using Diamonds.Common.Models;
using Diamonds.Common.Entities;

namespace Diamonds.Rest.Controllers
{
    [Route("api/[controller]")]
    public class BotsController : Controller
    {
        IStorage storage;

        public BotsController(IStorage storage)
        {
            this.storage = storage;
        }

        /// <summary>
        /// Get bot by id
        /// </summary>
        [ProducesResponseType(typeof(Bot), 200)]
        [ProducesResponseType(typeof(void), 404)]
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(string id)
        {
            var bot = storage.GetBot(id);

            if (bot == null)
            {
                return NotFound();
            }
            return Ok(bot);
        }

        /// <summary>
        /// Register bot
        /// </summary>
        /// <response code="409">Bot already exists</response>
        [ProducesResponseType(typeof(Bot), 200)]
        [ProducesResponseType(typeof(void), 409)]
        [Route("{id}")]
        [HttpPost]
        public IActionResult Post([FromBody] BotRegistrationInput input)
        {
            var bot = storage.GetBot(input);

            if (bot != null)
            {
                return StatusCode(409);
            }

            return Ok(storage.AddBot(input));
        }
    }
}
