using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Diamonds.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        public const string AppVersion = "1.33.7";

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(AppVersion);
        }

    }
}
