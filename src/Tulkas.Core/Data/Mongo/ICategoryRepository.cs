using System.Threading.Tasks;
using Tulkas.Core.Domain;

namespace Tulkas.Core.Data.Mongo
{
    public interface ICategoryRepository : IMongoBaseRepository<Category>
    {
        Task<Category> GetByName(string name);
    }
}