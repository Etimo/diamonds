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
        /// Get bot by token
        /// </summary>
        /// <remarks>
        /// If you forget the name of your bot, then you can get it here.
        /// </remarks>
        [ProducesResponseType(typeof(BotLight), 200)]
        [ProducesResponseType(typeof(void), 404)]
        [HttpGet]
        [Route("{token}")]
        public IActionResult Get(string token)
        {
            var bot = storage.GetBot(token);

            if (bot == null)
            {
                return NotFound();
            }

            var botLight = new BotLight()
            {
                Name = bot.Name
            };

            return Ok(botLight);
        }

        /// <summary>
        /// Register bot
        /// </summary>
        /// <response code="409">Bot already exists</response>
        [ProducesResponseType(typeof(Bot), 200)]
        [ProducesResponseType(typeof(void), 409)]
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
