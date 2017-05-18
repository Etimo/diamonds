﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diamonds.Api.Common.Entities
{
    public class Bot
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; } = Guid.NewGuid().ToString();
    }
}
