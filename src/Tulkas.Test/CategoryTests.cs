using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Tulkas.Core.Domain;
using Tulkas.Core.Services;
using Xunit;

namespace Tulkas.Test
{
    public class CategoryTests:TestBase
    {
        [Fact]
        public async void Should_Get_All_Categories()
        {
            var service = _serviceProvider.GetService<ICategoryService>();
            var categories = await service.Get();
            categories.Should().NotBeNull();
            categories.Count.Should().BeGreaterThan(0);
        }
        
        [Fact]
        public async void Should_Get_Category()
        {
            var service = _serviceProvider.GetService<ICategoryService>();
            var categories = await service.Get();
            categories.Should().NotBeNull();
            categories.Count.Should().BeGreaterThan(0);
            var id = categories.First().Id;
            var category = await service.Get(id);
            category.Should().NotBeNull();
            category.Id.Should().Be(id);
        }

        [Theory]
        [InlineData("Deneme Kategorisi", "Açıklama", false, true)]
        [InlineData("Türk Mutfağı", "Türk mutfağına ait lezzetler", false, false)]
        [InlineData("", "Açıklama", true, false)]
        public async void Should_Add_Category(string name, string description, bool expectation, bool delete)
        {
            var service = _serviceProvider.GetService<ICategoryService>();
            var category = new Category
            {
                Name = name,
                Description = description
            };
            category = await service.Add(category);
            category.Should().NotBeNull();
            string.IsNullOrEmpty(category.Id).Should().Be(expectation);
            if (delete)
                await service.Delete(category.Id);
        }
    }
}