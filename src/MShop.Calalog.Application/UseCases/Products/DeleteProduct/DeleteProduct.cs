using MediatR;
using MShop.Catalog.Domain.Respositories;
using MShop.Catalog.Infra.Data.ES.Repositoty;
using MShop.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MShop.Calalog.Application.UseCases.Products.DeleteProduct
{
    public class DeleteProduct : BaseUseCase, IRequestHandler<DeleteProductInput, bool>
    {
        IProductRepository _productRepository;
        public DeleteProduct(Core.Message.INotification notification, IProductRepository productRepository) : base(notification)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(DeleteProductInput request, CancellationToken cancellationToken)
        {
            if(request.Id == Guid.Empty)
            {
                Notify("Por favor informar um Id valido");
                return false;
            }

            var productDb = await _productRepository.GetById(request.Id);
            if(productDb == null)
            {
                Notify($"Produto com Id  {request.Id} não encontrado!");
                return false;
            }

            await _productRepository.DeleteById(productDb, cancellationToken);
            return true;
        }
    }
}
