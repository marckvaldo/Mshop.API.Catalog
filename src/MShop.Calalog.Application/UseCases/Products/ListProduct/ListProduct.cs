using MediatR;
using MShop.Calalog.Application.UseCases.Products.Commun;
using MShop.Catalog.Domain.Respositories;
using MShop.Core.Base;
using MShop.Core.Paginated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MShop.Calalog.Application.UseCases.Products.ListProduct
{
    public class ListProduct : BaseUseCase, IRequestHandler<ListProductInput, ListProductOutPut>
    {
        IProductRepository _productRepository;
        public ListProduct(Core.Message.INotification notification, IProductRepository productRepository) : base(notification)
        {
            _productRepository = productRepository;
        }

        public async Task<ListProductOutPut> Handle(ListProductInput request, CancellationToken cancellationToken)
        {
           var result =  await _productRepository.FilterPaginate(request, cancellationToken);

            return new ListProductOutPut(
                result.CurrentPage,
                result.PerPage, 
                result.Total, 
                result.Itens.Select(x =>new ProductOutPut(
                                            x.Id, 
                                            x.Description, 
                                            x.Name, 
                                            x.Price, 
                                            x.Stock, 
                                            x.IsSale,
                                            x.CategoryId, 
                                            x.Category.Name, 
                                            x.Thumb.Path)
                ).ToList());
        }
    }
}
