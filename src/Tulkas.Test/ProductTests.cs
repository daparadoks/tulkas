using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Tulkas.Core.Domain;
using Tulkas.Core.Services;
using Xunit;

namespace Tulkas.Test
{
    public class ProductTests:TestBase
    {
        [Fact]
        public async void Should_Get_All_Product()
        {
            var service = _serviceProvider.GetService<IProductService>();
            var products = await service.Get();
            products.Should().NotBeNull();
        }
        
        [Fact]
        public async void Should_Get_Product()
        {
            var service = _serviceProvider.GetService<IProductService>();
            var products = await service.Get();
            products.Should().NotBeNull();
            if(products.Count == 0)
                return;

            var id = products.First().Id;
            var product = await service.Get(id);
            product.Should().NotBeNull();
            product.Id.Should().Be(id);
            product.Category.Should().NotBeNull();
        }

        [Theory]
        [InlineData("Deneme", "Açıklama", "TL", 459.99, false, true)]
        [InlineData("Döner", "Açıklama", "TL", 60.00, false, false)]
        [InlineData("Kebap", "Açıklama", "TL", 60.00, false, false)]
        [InlineData("Lahmacun", "Açıklama", "TL", 10.00, false, false)]
        public async void Should_Add_Product(string name, string description, string currency, double price, bool expectation, bool delete)
        {
            var productService = _serviceProvider.GetService<IProductService>();
            var categoryService = _serviceProvider.GetService<ICategoryService>();

            var categories = await categoryService.Get();
            categories.Should().NotBeNull();
            var category = categories.FirstOrDefault();
            category.Should().NotBeNull();

            var product = new Product
            {
                Name = name,
                Description = description,
                CategoryId = category.Id,
                Currency = currency,
                Price = price
            };
            product = await productService.Add(product);
            string.IsNullOrEmpty(product.Id).Should().Be(expectation);

            if (delete)
                await productService.Delete(product.Id);
        }
        
        
    }
}