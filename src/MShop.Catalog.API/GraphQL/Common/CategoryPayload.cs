using MShop.Calalog.Application.UseCases.Category.Common;
using MShop.Core.DomainObject;

namespace MShop.Catalog.API.GraphQL.Common
{
    public class CategoryPayload
    {
        public CategoryPayload(Guid id, string name, bool isActive)
        {
            Id = id;
            Name = name;
            IsActive = isActive;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public bool IsActive { get; private set; }

        public static CategoryPayload FromCategoryModelOutPut(CategoryModelOutPut category)
            => new CategoryPayload(category.Id, category.Name, category.IsActive);

    }
}
