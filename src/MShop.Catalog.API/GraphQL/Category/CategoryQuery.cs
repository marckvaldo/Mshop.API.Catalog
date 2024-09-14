using HotChocolate.Language;
using MediatR;
using MShop.Calalog.Application.UseCases.Category.GetCategory;
using MShop.Calalog.Application.UseCases.Category.ListCategories;
using MShop.Catalog.API.GraphQL.Common;
using MShop.Core.Base;
using MShop.Core.Enum.Paginated;
using Message = MShop.Core.Message;

namespace MShop.Catalog.API.GraphQL.Category
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class CategoryQuery : BaseGraphQL
    {
        public async Task<CategoryPayload> GetCategoryById(Guid id, CancellationToken cancellationToken, [Service] IMediator mediator, [Service] Message.INotification notifications)
        {
            var result = await mediator.Send(new GetCategoryInput(id), cancellationToken);
            RequestIsValid(notifications);
            return CategoryPayload.FromCategoryModelOutPut(result);
        }

        public async Task<SearchCategoryPayload> ListCategoriesAsync(
            [Service] IMediator mediator,
            [Service] Message.INotification notifications,
            int page = 1,
            int perPage = 10,
            string search = "",
            string orderBy = "",
            SearchOrder order = SearchOrder.Asc,
            CancellationToken cancellationToken = default)
        {
            var input = new ListCategoryInput(page, perPage, search, orderBy, order);

            var output = await mediator.Send(input, cancellationToken);
            RequestIsValid(notifications);

            return new SearchCategoryPayload(
                output.Page,
                output.PerPage,
                output.Total,
                output.Itens.Select(x => new CategoryPayload(
                    x.Id,
                    x.Name,
                    x.IsActive)).ToList()
                    );
        }
        
    }


}
