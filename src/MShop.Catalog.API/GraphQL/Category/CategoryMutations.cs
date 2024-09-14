using MediatR;
using MShop.Calalog.Application.UseCases.Category.CreateCategory;
using MShop.Calalog.Application.UseCases.Category.DeleteCategory;
using MShop.Calalog.Application.UseCases.Category.GetCategory;
using MShop.Catalog.API.GraphQL.Common;
using Message = MShop.Core.Message;

namespace MShop.Catalog.API.GraphQL.Category
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public class CategoryMutations : BaseGraphQL
    {
        public async Task<CategoryPayload> SaveCategoryAsync(CreateCategoryInput input,
            [Service] IMediator mediator,
            [Service] Message.INotification notifications,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(input, cancellationToken);
            RequestIsValid(notifications);
            var category = await mediator.Send(new GetCategoryInput(input.Id), cancellationToken);
            RequestIsValid(notifications);
            var outPut = CategoryPayload.FromCategoryModelOutPut(category);
            return outPut;
            
        }

        public async Task<bool> DeleteCategoryAsync(Guid id, [Service] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(new DeleteCategoryInput(id), cancellationToken);  
        }

        
    }
}
