using Diamonds.Api.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diamonds.Api.Common.Storage
{
    public interface IStorage
    {
        Bot GetBot(BotRegistrationInput input);
        Bot GetBot(string token);
        Bot AddBot(BotRegistrationInput input);


    }
}
