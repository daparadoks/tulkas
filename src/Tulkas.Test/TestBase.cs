using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tulkas.Core.Data.Mongo;
using Tulkas.Core.Data.Redis;
using Tulkas.Core.Services;

namespace Tulkas.Test
{
    public class TestBase
    {
        protected readonly ServiceProvider _serviceProvider;
        public TestBase()
        {
            var services = new ServiceCollection();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            
            services.AddTransient<IRedisHelper, RedisHelper>();

            _serviceProvider = services.BuildServiceProvider();
        }
    }
}