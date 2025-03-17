using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using FinancialTransactionApp.Model;

namespace FinancialTransaction.Accessors
{

    public class MongoFinancialTransaction : IFianancialTransactionAcessor
    {
       
        private readonly IMongoCollection <FinancialTransactionType> _collection;
        private const string COLLECTION_NAME = "financialtransaction";

        public MongoFinancialTransaction(MongoDbContext mongoDbContext)
        {
   
            _collection = mongoDbContext.GetCollection<FinancialTransactionType>(COLLECTION_NAME);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var resultDelete = await _collection.DeleteOneAsync(t => t.Id == id);
            return resultDelete.DeletedCount > 0;

        }

        public async Task<IEnumerable<FinancialTransactionType>> FindAsync(int id)
        {
            var result = await _collection.FindAsync(transaction => transaction.Id == id);
            return result.ToEnumerable();
        }

        public async Task<bool> SaveAsync(FinancialTransactionType transaction)
        {
            var cursor = await _collection.FindAsync(t => t.Id == transaction.Id);

            if (!cursor.Any())
            {
                await _collection.InsertOneAsync(transaction);
                return true;
            }
            return false;

        }

        public async Task<bool> UpdateAsync(FinancialTransactionType transaction)
        {
            var resultUpdate = await _collection.ReplaceOneAsync(t => t.Id == transaction.Id, transaction);
            return resultUpdate.ModifiedCount > 0;
        }
    }
}
