using MediatR;
using MShop.Calalog.Application.UseCases.Products.Commun;
using MShop.Catalog.Domain.Respositories;
using MShop.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MShop.Calalog.Application.UseCases.Products.GetProduct
{
    public class GetProduct : BaseUseCase, IRequestHandler<GetProductInput, ProductOutPut>
    {
        IProductRepository _productRepository;
        public GetProduct(Core.Message.INotification notification, IProductRepository productRepository) : base(notification)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductOutPut> Handle(GetProductInput request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                Notify("Por favor informe um Id Valido");
                return ProductOutPut.Error();
            }    

            var result =  await _productRepository.GetById(request.Id);
            if (result == null)
            {
                Notify("Produto Não Encontrado!");
                return ProductOutPut.Error();
            }

            return ProductOutPut.FromEntity(result);
        }
    }
}
