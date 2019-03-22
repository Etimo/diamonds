using System.Collections.Generic;
using Diamonds.Common.Entities;
using Diamonds.Common.Enums;
using Diamonds.Common.Storage;
using Microsoft.AspNetCore.Mvc;

namespace Diamonds.Rest.Controllers
{
    [Route("api/[controller]")]
    public class HighscoreController : Controller
    {

        IStorage _storage;

        public HighscoreController(IStorage storage)
        {
            this._storage = storage;
        }

        /// <summary>
        /// Get highsores
        /// </summary>
        [ProducesResponseType(typeof(IEnumerable<Highscore>), 200)]
        [ProducesResponseType(typeof(void), 204)]
        [HttpGet]
        public IActionResult Get(SeasonSelector season = SeasonSelector.Current)
        {
            var highscores = _storage.GetHighscores(season);

            if (highscores == null) {
                return NoContent();
            }

            return Ok(highscores);
        }
    }
}
