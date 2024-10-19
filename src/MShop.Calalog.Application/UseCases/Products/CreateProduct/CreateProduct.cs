using MediatR;
using MShop.Catalog.Domain.Respositories;
using MShop.Core.Base;
using Entity = MShop.Catalog.Domain.Entity;

namespace MShop.Calalog.Application.UseCases.Products.CreateProduct
{
    public class CreateProduct : BaseUseCase, IRequestHandler<CreateProductInput,bool>
    {
        private IProductRepository _productRepository;
      
        public CreateProduct(Core.Message.INotification notification, IProductRepository productRepository) : base(notification)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(CreateProductInput request, CancellationToken cancellationToken)
        {
            var produtos = new Entity.Product(request.Id, request.Description, request.Name, request.Price, request.Thumb, request.Stock);
            
            if (!Validate(produtos.IsValid())) return false;

            return await _productRepository.Create(produtos, cancellationToken);

        }
    }
}
