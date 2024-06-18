using MediatR;
using MShop.Calalog.Application.UseCases.Category.Common;
using MShop.Catalog.Domain.Respositories;
using MShop.Core.Base;
using System.Reflection.Metadata.Ecma335;
using CoreMessage = MShop.Core.Message;
using DominEntity = MShop.Catalog.Domain.Entity;

namespace MShop.Calalog.Application.UseCases.Category.CreateCategory
{
    public class CreateCategory : BaseUseCase, IRequestHandler<CreateCategoryInPut, bool>
    {
        private ICategoryRepository _categoryRepository;
        public CreateCategory(ICategoryRepository categoryRepository,
            CoreMessage.INotification _notification) : base(_notification)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> Handle(CreateCategoryInPut request, CancellationToken cancellationToken)
        {
            var category = new DominEntity.Category(request.Name, request.Id, request.IsActive);
            if(!Validate(category.IsValid())) return false;
            await _categoryRepository.Create(category,cancellationToken);
            return true;

        }

       
    }
}
