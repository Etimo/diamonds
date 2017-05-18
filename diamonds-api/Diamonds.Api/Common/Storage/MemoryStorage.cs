using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diamonds.Api.Common.Entities;

namespace Diamonds.Api.Common.Storage
{
    public class MemoryStorage : IStorage
    {
        private List<Bot> bots = new List<Bot>();

        public Bot AddBot(BotRegistrationInput input)
        {
            var bot = new Bot
            {
                Name = input.Name,
                Email = input.Email
            };
            bots.Add(bot);
            return bot;
        }

        public Bot GetBot(BotRegistrationInput input)
        {
            return bots.SingleOrDefault<Bot>(bot => bot.Name.Equals(input.Name) || bot.Email.Equals(input.Email));
        }

        public Bot GetBot(string token)
        {
            return bots.SingleOrDefault<Bot>(bot => bot.Token.Equals(token));
        }

    }
}
