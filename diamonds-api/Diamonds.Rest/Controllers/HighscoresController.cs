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

        [HttpGet]
        public IActionResult Get()
        {
            var highscores = _storage.GetHighscores();
            
            if (highscores == null) {
                return NoContent();
            }

            return Ok(highscores);
        }
    }
}