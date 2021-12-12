using System.Collections.Generic;
using System.Threading.Tasks;
using Tulkas.Core.BusinessObjects;

namespace Tulkas.Core.Services
{
    public interface IServiceBase<T>
    {
        Task<IList<CategoryBo>> Get();
        Task<CategoryBo> Get(string id);
        Task<T> Add(T entity);
        Task Update(string id, T entity);
        Task Delete(string id);
    }
}