using MediatR;

namespace MShop.Calalog.Application.UseCases.Category.DeleteCategory
{
    public class DeleteCategoryInput : IRequest<bool>
    {
        public Guid Id {  get; set; }

        public DeleteCategoryInput(Guid id)
        {
            Id = id;
        }
    }
}
