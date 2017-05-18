using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diamonds.Common.Models
{
    public class BotRegistrationOutput
    {
        public string Token { get; set; } = Guid.NewGuid().ToString();
    }
}
