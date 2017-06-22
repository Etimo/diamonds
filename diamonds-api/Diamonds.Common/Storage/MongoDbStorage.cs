using System;
using System.Collections.Generic;
using Diamonds.Common.Entities;
using Diamonds.Common.Models;
using MongoDB.Driver;

namespace Diamonds.Common.Storage
{

    public class MongoDBStorage : IStorage
    {
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
            }
            catch (Exception ex)
            {
                throw new Exception("Can not access to db server.", ex);
            }
        }

        public Bot GetBot(BotRegistrationInput input)
        {
            throw new NotImplementedException();
        }

        public Bot GetBot(string token)
        {
            throw new NotImplementedException();
        }

        public Bot AddBot(BotRegistrationInput input)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Board> GetBoards()
        {
            var collection = _database.GetCollection<Board>("boards");
            var result = collection.Find(m => true);
            
            var boards = result.ToList();
            return boards;
        }

        public Board GetBoard(string id)
        {
            throw new NotImplementedException();
        }
    }
}