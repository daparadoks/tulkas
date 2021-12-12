using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tulkas.Core.BusinessObjects;
using Tulkas.Core.Constants;
using Tulkas.Core.Converters;
using Tulkas.Core.Data.Mongo;
using Tulkas.Core.Data.Redis;
using Tulkas.Core.Domain;

namespace Tulkas.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRedisHelper _redis;
        private readonly ICategoryRepository _repository;

        public CategoryService(IRedisHelper redis, ICategoryRepository repository)
        {
            _redis = redis;
            _repository = repository;
        }

        public async Task<IList<CategoryBo>> Get()
        {
            var categories = await _repository.Get();
            return categories.Select(x => x.ToBo()).ToList();
        }

        public async Task<CategoryBo> Get(string id) => await _redis.GetOrAdd(CacheKeysConstants.Category(id),
            async () => await GetCategory(id));

        private async Task<CategoryBo> GetCategory(string id)
        {
            var category = await _repository.Get(id);
            return category.ToBo();
        }

        public async Task<Category> Add(Category category)
        {
            if (string.IsNullOrEmpty(category.Name))
                return category;
            var existsCategory = await _repository.GetByName(category.Name);
            if (existsCategory != null)
                return existsCategory;
            
            category = await _repository.Add(category);
            if (string.IsNullOrEmpty(category.Id))
                return category;
            
            await _redis.Add(CacheKeysConstants.Category(category.Id), category);
            return category;
        }

        public async Task Update(string id, Category category)
        {
            if(category == null || string.IsNullOrEmpty(id))
                return;
            
            category.Id = id;
            var dbTask = _repository.Update(category);
            var redisTask = _redis.Add(CacheKeysConstants.Category(category.Id), category);
            await Task.WhenAll(dbTask, redisTask);
        }

        public async Task Delete(string id)
        {
            var dbTask = _repository.Delete(id);
            var redisTask = _redis.Delete(CacheKeysConstants.Category(id).Key);
            await Task.WhenAll(dbTask, redisTask);
        }
    }
}