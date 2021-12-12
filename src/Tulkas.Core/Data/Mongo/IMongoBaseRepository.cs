using System.Collections.Generic;
using System.Threading.Tasks;
using Tulkas.Core.Domain;

namespace Tulkas.Core.Data.Mongo
{
    public interface IMongoBaseRepository<T> where T: MongoDomainBase
    {
        Task<T> Get(string id);
        Task<IList<T>> Get();
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(string id);
    }
}