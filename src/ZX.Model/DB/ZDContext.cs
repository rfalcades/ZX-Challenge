using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ZX.Model.DB
{
    public class ZDContext : IDbContext
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public ZDContext(IConfiguration config) : 
            this(config.GetConnectionString("ZXDB"))
        {

        }

        public ZDContext(string connectionString)
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(new MongoUrl(connectionString).DatabaseName);
        }

        public IMongoClient Client
        {
            get { return _client; }
        }

        public IMongoCollection<Model.PDV> PDVs
        {
            get { return _database.GetCollection<Model.PDV>("PDV"); }
        }
    }

    
}
