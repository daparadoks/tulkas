using System.Threading.Tasks;
using MongoDB.Driver;
using Tulkas.Core.Domain;

namespace Tulkas.Core.Data.Mongo
{
    public class CategoryRepository : MongoBaseRepository<Category>, ICategoryRepository
    {
        public async Task<Category> GetByName(string name) =>
            await _collection.Find(x => x.Name == name).FirstOrDefaultAsync();
    }
}