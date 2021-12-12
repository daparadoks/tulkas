namespace Tulkas.Core.Domain
{
    public class Product:MongoDomainBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
    }
}