using MongoDB.Driver;

namespace FinancialTransaction.Accessors
{
    public class MongoDbContext
    {
       
        private readonly IMongoDatabase _database;
        
        
        public MongoDbContext(MongoDbSettings mongoDbSettings) {

            var client = new MongoClient(mongoDbSettings.ConnectionString);
            var _database = client.GetDatabase(mongoDbSettings.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}
