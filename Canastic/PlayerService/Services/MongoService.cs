using MongoDB.Driver;
using PlayerService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.IO;
using MongoDB.Bson;
using System.Configuration;

namespace PlayerService.Services
{
    public class MongoService : IMongoService
    {
        IMongoDatabase _db;
        public MongoService(string database)
        {
            //MongoClient client = new(ConfigurationManager.ConnectionStrings["MongoDBConn"].ConnectionString);
            MongoClient client = new(GetConnection().GetSection("ConnectionStrings").GetSection("MongoDBConn").Value);
            _db = client.GetDatabase(database);
        }

        private IConfigurationRoot GetConnection()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            return builder;
        }

        public void DeleteRecord<T>(string table, Guid id)
        {
            var collection = _db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            collection.DeleteOne(filter);
        }

        public async void InsertRecord<T>(string table, T record)
        {
            var collection = _db.GetCollection<T>(table);
            await collection.InsertOneAsync(record);
        }

        public T LoadRecordByID<T>(string table, Guid id)
        {
            var collection = _db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            return collection.Find(filter).First();
        }

        public List<T> LoadRecords<T>(string table)
        {
            var collection = _db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();
        }

        public void UpsertRecord<T>(string table, Guid id, T record)
        {
            BsonBinaryData binData = new(id, GuidRepresentation.Standard);
            var collection = _db.GetCollection<T>(table);
            var result = collection.ReplaceOne(
                new BsonDocument("_id", binData),
                record,
                new ReplaceOptions { IsUpsert = true });
        }
    }
}
