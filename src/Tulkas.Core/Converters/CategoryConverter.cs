using Tulkas.Core.BusinessObjects;
using Tulkas.Core.Domain;

namespace Tulkas.Core.Converters
{
    public static class CategoryConverter
    {
        public static CategoryBo ToBo(this Category category)
        {
            return new CategoryBo
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }
    }
}