using System;
using System.Collections.Generic;
using Diamonds.Common.Entities;
using Diamonds.Common.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Diamonds.Common.Storage
{

    public class MongoDBStorage : IStorage
    {
        private readonly Board _defaultBoard = new Board
        {
            Id = "1",
            Height = 10,
            Width = 10,
            Bots = new List<BoardBot>(),
            Diamonds = new List<Position>()
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

                var board = GetBoard("1");
                if (board == null) CreateBoard(_defaultBoard);
            }
            catch (Exception ex)
            {
                throw new Exception("Can not access to db server.", ex);
            }
        }

        public Bot GetBot(BotRegistrationInput input)
        {
            var collection = _database.GetCollection<Bot>("Bots");
            var result = collection.Find(m => m.Name.Equals(input.Name) || m.Email.Equals(input.Email)).SingleOrDefault();
            return result;
        }

        public Bot GetBot(string token)
        {
            var collection = _database.GetCollection<Bot>("Bots");
            var result = collection.Find(m => m.Token.Equals(token)).SingleOrDefault();
            return result;
        }

        public Bot AddBot(BotRegistrationInput input)
        {
            Bot bot = new Bot
            {
                Name = input.Name,
                Email = input.Email
            };
            _database.GetCollection<Bot>("Bots").InsertOne(bot);
            return bot;
        }

        public IEnumerable<Board> GetBoards()
        {
            var collection = _database.GetCollection<Board>("Boards");
            var result = collection.Find(m => true);
            var boards = result.ToList();
            return boards;
        }

        public Board GetBoard(string id)
        {
            var collection = _database.GetCollection<Board>("Boards");
            var result = collection.Find(m => m.Id.Equals(id)).Single();
            return result;
        }

        public void CreateBoard(Board board)
        {
            var collection = _database.GetCollection<Board>("Boards");
            collection.InsertOne(board);
        }

        public void UpdateBoard(Board board)
        {
            var collection = _database.GetCollection<Board>("Boards");
            collection.ReplaceOne(
                new BsonDocument("_id", board.Id),
                board,
                new UpdateOptions { IsUpsert = true }
            );
        }
    }
}