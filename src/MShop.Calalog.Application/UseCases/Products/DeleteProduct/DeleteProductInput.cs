using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MShop.Calalog.Application.UseCases.Products.DeleteProduct
{
    public record DeleteProductInput(Guid Id) : IRequest<bool>;

}
