using System.Collections.Generic;
using System.Threading.Tasks;
using Tulkas.Core.BusinessObjects;
using Tulkas.Core.Domain;

namespace Tulkas.Core.Services
{
    public interface IProductService
    {
        Task<IList<ProductListItem>> Get();
        Task<ProductDetail> Get(string id);
        Task<Product> Add(Product product);
        Task Update(string id, Product product);
        Task Delete(string id);
    }
}