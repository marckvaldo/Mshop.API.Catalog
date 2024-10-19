using MediatR;
using MShop.Core.Enum.Paginated;
using MShop.Core.Paginated;

namespace MShop.Calalog.Application.UseCases.Products.ListProduct
{
    public class ListProductInput : PaginatedInPut, IRequest<ListProductOutPut>
    {
        public ListProductInput(
            int page = 1,
            int perPage = 20,
            string search = "",
            string orderBy = "",
            SearchOrder order = SearchOrder.Asc) : base(page, perPage, search, orderBy, order)
        {
        }
    }
}
