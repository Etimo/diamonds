using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diamonds.Common.Entities;
using Diamonds.Common.Models;

namespace Diamonds.Common.Storage
{
    public class MemoryStorage : IStorage
    {
        private List<Bot> bots = new List<Bot>();
        private List<Board> _boards = new List<Board>
        {
            new Board
            {
                BoardId = "1", Height = 10, Width = 10,
                Bots = new List<BoardBot>{
                    new BoardBot {
                        Name = "Jane Jet",
                        Base = new Position { X = 6, Y = 2 },
                        Position = new Position { X = 8, Y = 0 },
                        Score = 5,
                        Diamonds = 0,
                        MillisecondsLeft = 5 * 60 * 1000
                    },
                    new BoardBot {
                        Name = "Johnny Rucker",
                        Base = new Position { X = 5, Y = 8 },
                        Position = new Position { X = 0, Y = 0 },
                        Score = 2,
                        Diamonds = 2,
                        MillisecondsLeft = 5 * 60 * 1000
                    },
                    new BoardBot {
                        Name = "Elton Jan",
                        Base = new Position { X = 7, Y = 9 },
                        Position = new Position { X = 2, Y = 3 },
                        Score = 8,
                        Diamonds = 5,
                        MillisecondsLeft = 5 * 60 * 1000
                    }
                },
                Diamonds = new List<Position>
                {
                    new Position { X = 1, Y = 1 },
                    new Position { X = 0, Y = 0 },
                    new Position { X = 8, Y = 9 },
                    new Position { X = 9, Y = 9 },
                    new Position { X = 5, Y = 1 },
                    new Position { X = 6, Y = 2 },
                    new Position { X = 1, Y = 9 }
                }
            }
        };

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

        public IEnumerable<Board> GetBoards()
        {
            return _boards;
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
