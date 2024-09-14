using MShop.Calalog.Application.Common;
using MShop.Calalog.Application.UseCases.Category.Common;
using MShop.Catalog.API.GraphQL.Common;
using MShop.Core.DomainObject;

namespace MShop.Catalog.API.GraphQL.Category
{
    public class SearchCategoryPayload : PaginatedListOutPut<CategoryPayload>
    {
        public SearchCategoryPayload(int page, int perPage, int total, IReadOnlyList<CategoryPayload> itens) : base(page, perPage, total, itens)
        {

        }
    }
}
