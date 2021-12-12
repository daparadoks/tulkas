using Tulkas.Core.Converters;
using Tulkas.Core.Domain;

namespace Tulkas.Core.BusinessObjects
{
    public class ProductDetail:ProductBo
    {
        public ProductDetail()
        {
            
        }
        public ProductDetail(ProductBo product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            Currency = product.Currency;
        }

        public CategoryBo Category { get; set; }
    }

    public class ProductListItem:ProductBo
    {
        public ProductListItem()
        {
            
        }
        public ProductListItem(ProductBo product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            Currency = product.Currency;
        }

        public string CategoryId { get; set; }
    }

    public class ProductBo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        
    }
}