using MediatR;
using MShop.Core.Enum.Paginated;
using MShop.Core.Paginated;

namespace MShop.Calalog.Application.UseCases.Category.ListCategories
{
    public class ListCategoryInput : PaginatedInPut, IRequest<ListCategoriesOutput>
    {
        public ListCategoryInput(
            int page = 1, 
            int perPage = 20, 
            string search = "",
            string orderBy = "", 
            SearchOrder order = SearchOrder.Asc) : base(page,perPage,search,orderBy,order) { }
    }
}
