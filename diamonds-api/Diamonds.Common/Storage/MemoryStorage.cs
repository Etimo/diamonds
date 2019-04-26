using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diamonds.Common.Entities;
using Diamonds.Common.Enums;
using Diamonds.Common.Models;
using Diamonds.Common.GameEngine.GameObjects;

namespace Diamonds.Common.Storage
{

    public class MemoryStorage : IStorage
    {
        private IEnumerable<Bot> _bots = new List<Bot>
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
                Id = "1", Height = 10, Width = 15,
                GameObjects = new List<BaseGameObject>{},
                Bots = new List<BoardBot>{
                    new BoardBot {
                        Name = "Jane Jet",
                        Base = new Position(6, 2),
                        Position = new Position(8, 0),
                        Score = 5,
                        Diamonds = 0,
                    },
                    new BoardBot {
                        Name = "indrif",
                        Base = new Position(5, 8),
                        Position = new Position(0, 0),
                        Score = 2,
                        Diamonds = 2,
                    },
                    new BoardBot {
                        Name = "Elton Jan",
                        Base = new Position(7, 9),
                        Position = new Position(2, 3),
                        Score = 8,
                        Diamonds = 5,
                    }
                },
                Diamonds = new List<DiamondPosition>
                {
                    new DiamondPosition(1, 1),
                    new DiamondPosition(0, 0),
                    new DiamondPosition(8, 9),
                    new DiamondPosition(9, 9),
                    new DiamondPosition(5, 1),
                    new DiamondPosition(6, 2),
                    new DiamondPosition(1, 9),
                    new DiamondPosition(5, 5, 2)
                }
            }
        };

        public Task<Bot> AddBotAsync(BotRegistrationInput input)
        {
            var bot = new Bot
            {
                Name = input.Name,
                Email = input.Email
            };
            (_bots as List<Bot>)?.Add(bot);
            return Task.FromResult(bot);
        }

        public Task<IEnumerable<Board>> GetBoardsAsync()
        {
            return Task.FromResult(_boards as IEnumerable<Board>);
        }

        public Task<Board> GetBoardAsync(string id)
        {
            return Task.FromResult(_boards.SingleOrDefault<Board>(board => board.Id.Equals(id)));
        }

        public Task<Bot> GetBotAsync(BotRegistrationInput input)
        {
            return Task.FromResult(_bots.SingleOrDefault<Bot>(bot => bot.Name.Equals(input.Name) || bot.Email.Equals(input.Email)));
        }

        public Task<Bot> GetBotAsync(string token)
        {
            return Task.FromResult(_bots.SingleOrDefault<Bot>(bot => bot.Token.Equals(token)));
        }

        public Task UpdateBoardAsync(Board board)
        {
            _boards.Remove(board);
            _boards.Add(board);

            return Task.FromResult(new object());
        }

        public Task CreateBoardAsync(Board board)
        {
            _boards.Add(board);

            return Task.FromResult(new object());
        }

        public Task<IEnumerable<Highscore>> GetHighscoresAsync(SeasonSelector season, string botName = null)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBotAsync(Bot bot)
        {
            throw new NotImplementedException();
        }

        public Task SaveHighscoreAsync(Highscore score)
        {
            throw new NotImplementedException();
        }
    }
}
