using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diamonds.Common.Entities
{
    public class Bot
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; } = Guid.NewGuid().ToString();
        public string BoardToken { get; set; }
    }
}
