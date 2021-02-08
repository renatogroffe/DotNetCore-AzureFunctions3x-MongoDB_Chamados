using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using ServerlessSuporte.Documents;

namespace ServerlessSuporte.Data
{
    public class ChamadoRepository
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<ChamadoDocument> _collection;
        
        public ChamadoRepository()
        {
            _client = new MongoClient(
                Environment.GetEnvironmentVariable("MongoDBConnection"));
            _database = _client.GetDatabase("DBChamados");
            _collection =_database.GetCollection<ChamadoDocument>("Chamados");
        }

        public void Save(ChamadoDocument document)
        {
            _collection.InsertOne(document);
        }       

        public List<ChamadoDocument> ListAll()
        {
            return _collection.Find(all => true).ToList();
        }
    }
}