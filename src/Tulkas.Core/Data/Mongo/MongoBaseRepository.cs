using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Tulkas.Core.Domain;

namespace Tulkas.Core.Data.Mongo
{
    public class MongoBaseRepository<T>: IMongoBaseRepository<T> where T: MongoDomainBase
    {
        protected readonly IMongoCollection<T> _collection;
        public MongoBaseRepository()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("local");

            _collection = database.GetCollection<T>(typeof(T).Name);
        }

        public async Task<T> Get(string id) => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task<IList<T>> Get() => await _collection.Find(x => true).ToListAsync();

        public async Task<T> Add(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task Update(T entity) => await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        public async Task Delete(string id) => await _collection.DeleteOneAsync(x => x.Id == id);
    }
}