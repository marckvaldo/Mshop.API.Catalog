using MediatR;
using MShop.Calalog.Application.UseCases.Products.Commun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MShop.Calalog.Application.UseCases.Products.GetProduct
{
    public record GetProductInput(Guid Id) : IRequest<ProductOutPut>;
}
