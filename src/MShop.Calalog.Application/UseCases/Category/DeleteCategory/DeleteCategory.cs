using MediatR;
using MShop.Calalog.Application.UseCases.Category.DeleteCategory;
using MShop.Catalog.Domain.Respositories;
using MShop.Core.Base;
using CoreMessage = MShop.Core.Message;

namespace MShop.Calalog.Application.UseCases.Category.DeleteCategory
{
    public class DeleteCategory : BaseUseCase, IRequestHandler<DeleteCategoryInput, bool>
    {
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;
        public DeleteCategory(ICategoryRepository categoryRepository,
            IProductRepository productRepositorym,
            CoreMessage.INotification notification) : base(notification)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepositorym;
        }

        public async Task<bool> Handle(DeleteCategoryInput request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty) 
            {
                Notify("Id não pode ser vazio");
                return false;
            }

            var categoryDb = await _categoryRepository.GetById(request.Id);
            if (categoryDb is null)
            {
                Notify($"Categoria com Id {request.Id} não encontrada na base de dados!");
                return false;
            }

            var isThereProduct = await _productRepository.GetProductsByCategoryId(request.Id);
            if(isThereProduct?.Count() > 0) 
            {
                Notify($"Não é possivel deletar uma categoria quando a mesma exitem produtos relacionada!");
                return false;
            }

            await _categoryRepository.DeleteById(categoryDb, cancellationToken);
            return true;
        }
    }
}
