using Tulkas.Core.BusinessObjects;
using Tulkas.Core.Domain;

namespace Tulkas.Core.Converters
{
    public static class ProductConverter
    {
        public static ProductBo ToBo(this Product product)
        {
            return new ProductBo
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Currency = product.Currency,
                Price = product.Price
            };
        }

        public static ProductListItem ToListItem(this Product product)
        {
            return new ProductListItem(product.ToBo())
            {
                CategoryId = product.CategoryId
            };
        }

        public static ProductDetail ToDetail(this Product product, Category category)
        {
            return new ProductDetail(product.ToBo())
            {
                Category = category.ToBo()
            };
        }
    }
}