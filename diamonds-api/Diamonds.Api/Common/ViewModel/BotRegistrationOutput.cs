using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diamonds.Api.Common.Entities
{
    public class BotRegistrationOutput
    {
        public string Token { get; set; } = Guid.NewGuid().ToString();
    }
}
