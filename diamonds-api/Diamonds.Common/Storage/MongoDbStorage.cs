using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diamonds.Common.Entities;
using Diamonds.Common.Enums;
using Diamonds.Common.Models;
using Diamonds.Common.GameEngine.GameObjects;
using Diamonds.Common.GameEngine.GameObjects.Teleporters;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Diamonds.Common.Storage
{

    public class MongoDBStorage : IStorage
    {
        private readonly Board _defaultBoard = new Board
        {
            Id = "1",
            Height = 10,
            Width = 15,
            Bots = new List<BoardBot>(),
            Diamonds = new List<DiamondPosition>(),
            GameObjects = new List<BaseGameObject>()
        };

        public static string ConnectionString { get; set; }
        public static string DatabaseName { get; set; }
        public static bool IsSSL { get; set; }

        private IMongoDatabase _database { get; }

        public MongoDBStorage()
        {
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
                if (IsSSL)
                {
                    settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                }

                var mongoClient = new MongoClient(settings);
                _database = mongoClient.GetDatabase(DatabaseName);

                var board = GetBoardAsync("1").Result;
                if (board == null) CreateBoardAsync(_defaultBoard).Wait();
            }
            catch (Exception ex)
            {
                throw new Exception("Storage error.", ex);
            }
        }

        public async Task<Bot> GetBotAsync(BotRegistrationInput input)
        {
            var collection = _database.GetCollection<Bot>("Bots");
            var result = (await collection.FindAsync(m =>
                    m.Name.Equals(input.Name) || m.Email.Equals(input.Email)))
                .SingleOrDefault();
            return result;
        }

        public async Task<Bot> GetBotAsync(string token)
        {
            var collection = _database.GetCollection<Bot>("Bots");
            var result = (await collection.FindAsync(m => m.Token.Equals(token))).SingleOrDefault();
            return result;
        }

        public async Task<Bot> AddBotAsync(BotRegistrationInput input)
        {
            Bot bot = new Bot
            {
                Name = input.Name,
                Email = input.Email
            };
            await _database.GetCollection<Bot>("Bots").InsertOneAsync(bot);
            return bot;
        }

        public async Task<IEnumerable<Board>> GetBoardsAsync()
        {
            var collection = _database.GetCollection<Board>("Boards");
            var result = await collection.FindAsync(m => true);
            var boards = result.ToList();
            return boards;
        }

        public async Task<Board> GetBoardAsync(string id)
        {
            var collection = _database.GetCollection<Board>("Boards");
            var result = (await collection.FindAsync(m => m.Id.Equals(id))).SingleOrDefault();
            return result;
        }

        public async Task CreateBoardAsync(Board board)
        {
            var collection = _database.GetCollection<Board>("Boards");
            try {
                await collection.InsertOneAsync(board);
            } catch (MongoWriteException e) {
                // More than one thread creating default board, ignore for now
                Console.WriteLine(e);
            }
        }

        public async Task UpdateBoardAsync(Board board)
        {
            var collection = _database.GetCollection<Board>("Boards");
            await collection.ReplaceOneAsync(
                new BsonDocument("_id", board.Id),
                board,
                new UpdateOptions { IsUpsert = true }
            );
        }

        public Task<IEnumerable<Highscore>> GetHighscoresAsync(SeasonSelector season, string botName = null)
        {
            var seasonPeriod = Season.SeasonPeriod(season);
            var seasonBeginning = seasonPeriod.Item1;
            var seasonEnd = seasonPeriod.Item2;
            var collection = _database.GetCollection<Highscore>("Highscores");
            var highscores = collection
                .Aggregate(new AggregateOptions
                {
                    AllowDiskUse = true,
                })
                .Match(highscore => botName == null || highscore.BotName == botName)
                .Match(highscore => seasonBeginning == null || highscore.SessionFinishedAt >= seasonBeginning)
                .Match(highscore => seasonEnd == null || highscore.SessionFinishedAt <= seasonEnd)
                // Get the top score for each bot
                .SortByDescending(highscore => highscore.Score)
                // NOTE: AFAICT This is equivalent to
                // .Group(highscore => highscore.BotName, group => group.First())
                // But for some reason that throws a NotSupportedException
                // Maybe it requires a newer Mongo server?
                .Group("{_id: \"$BotName\", TopScore: {$first: \"$$ROOT\"}}")
                .ReplaceRoot<Highscore>("$TopScore")
                // Re-sort (since the Group steps destroys the sorting)
                .SortByDescending(highscore => highscore.Score)
                .Limit(30)
                .ToCursor()
                .ToEnumerable();
            return Task.FromResult(highscores);
        }

        public async Task UpdateBotAsync(Bot bot)
        {
            var collection = _database.GetCollection<Bot>("Bots");
            await collection.ReplaceOneAsync(
                new BsonDocument("_id", bot.Id),
                bot,
                new UpdateOptions { IsUpsert = true }
            );
        }

        public async Task SaveHighscoreAsync(Highscore score)
        {
            if(string.IsNullOrWhiteSpace(score.Id)) {
                score.Id = Guid.NewGuid().ToString();
            }

            var collection = _database.GetCollection<Highscore>("Highscores");
            await collection.ReplaceOneAsync(
                new BsonDocument("_id", score.Id),
                score,
                new UpdateOptions { IsUpsert = true }
            );
        }

    }
}
