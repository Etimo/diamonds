using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Diamonds.Api.Common.Entities;
using Diamonds.Api.Common.Storage;

namespace Diamonds.Api.Controllers
{
    [Route("[controller]")]
    public class BotsController : Controller
    {
        IStorage storage;

        public BotsController(IStorage storage)
        {
            this.storage = storage;
        }

        [HttpPost]
        public IActionResult Post(BotRegistrationInput input)
        {
            var bot = storage.GetBot(input);
            
            if (bot == null)
            {
                return StatusCode(409);
            }

            return Ok(storage.AddBot(input));
        }

    }
}
