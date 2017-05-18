using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Common.Entities;

namespace Diamonds.Api.Controllers
{
    [Route("[controller]")]
    public class BoardsController : Controller
    {
        public const int BoardId = 1;

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new List<Board>
                {
                    new Board
                    {
                        BoardId = "77", Height = 99, Width = 99,
                        Bots = new List<Bot>(),
                        Diamonds = new List<Position>()
                }
            });
        }

    }
}
