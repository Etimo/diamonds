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
        private List<Bot> _bots = new List<Bot>
        {
            new Bot
            {
                Name = "indrif",
                Email = "daniel.winther@etimo.se",
                Token = "d25b58a2-dda0-476e-b53b-65c080e9e267"
            }
        };
        private List<Board> _boards = new List<Board>
        {
            new Board
            {
                Id = "1", Height = 10, Width = 10,
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
                        Name = "indrif",
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
            _bots.Add(bot);
            return bot;
        }

        public IEnumerable<Board> GetBoards()
        {
            return _boards;
        }

        public Board GetBoard(string id)
        {
            return _boards.SingleOrDefault<Board>(board => board.Id.Equals(id));
        }

        public Bot GetBot(BotRegistrationInput input)
        {
            return _bots.SingleOrDefault<Bot>(bot => bot.Name.Equals(input.Name) || bot.Email.Equals(input.Email));
        }

        public Bot GetBot(string token)
        {
            return _bots.SingleOrDefault<Bot>(bot => bot.Token.Equals(token));
        }
        
        public void UpdateBoard(Board board)
        {
            _boards.Remove(board);
            _boards.Add(board);
        }

        public void CreateBoard(Board board)
        {
            _boards.Add(board);
        }

        public IEnumerable<Highscore> GetHighscores()
        {
            throw new NotImplementedException();
        }
    }
}
