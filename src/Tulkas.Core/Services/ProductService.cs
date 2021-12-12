using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tulkas.Core.BusinessObjects;
using Tulkas.Core.Components;
using Tulkas.Core.Constants;
using Tulkas.Core.Converters;
using Tulkas.Core.Data.Mongo;
using Tulkas.Core.Data.Redis;
using Tulkas.Core.Domain;

namespace Tulkas.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IRedisHelper _redis;
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IRedisHelper redis, IProductRepository repository, ICategoryRepository categoryRepository)
        {
            _redis = redis;
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IList<ProductListItem>> Get()
        {
            var products = await _repository.Get();
            return products.Select(x => x.ToListItem()).ToList();
        }

        public async Task<ProductDetail> Get(string id) => await _redis.GetOrAdd(CacheKeysConstants.Product(id),
            async () => await GetDetail(id));

        private async Task<ProductDetail> GetDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new CustomInformationException(ExceptionConstants.Required("Id değeri"));
            
            var product = await _repository.Get(id);
            if (product == null)
                throw new CustomInformationException(ExceptionConstants.NotFound("Ürün"));
            
            var category = await _categoryRepository.Get(product.CategoryId);
            return product.ToDetail(category);
        }

        public async Task<Product> Add(Product product)
        {
            product = await _repository.Add(product);
            if (string.IsNullOrEmpty(product.Id))
                throw new CustomInformationException(ExceptionConstants.ActionFailed("Ürün ekleme"));

            await _redis.Add(CacheKeysConstants.Product(product.Id), product);
            return product;
        }

        public async Task Update(string id, Product product)
        {
            if(product == null || string.IsNullOrEmpty(id))
                throw new CustomInformationException(ExceptionConstants.InvalidRequest());
            
            product.Id = id;
            var dbTask = _repository.Update(product);
            var redisTask = _redis.Add(CacheKeysConstants.Product(product.Id), product);
            await Task.WhenAll(dbTask, redisTask);
        }

        public async Task Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new CustomInformationException(ExceptionConstants.Required("Ürün id"));

            var dbTask = _repository.Delete(id);
            var redisTask = _redis.Delete(CacheKeysConstants.Product(id).Key);
            await Task.WhenAll(dbTask, redisTask);
        }
    }
}