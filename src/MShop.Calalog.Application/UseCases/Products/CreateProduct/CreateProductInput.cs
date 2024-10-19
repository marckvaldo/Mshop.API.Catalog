using MediatR;
using MShop.Business.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MShop.Calalog.Application.UseCases.Products.CreateProduct
{
    public record CreateProductInput(Guid Id, string Description, 
        string Name, 
        decimal Price, 
        decimal Stock, 
        bool IsSale, 
        Guid CategoryId, 
        string Thumb, 
        string Category) : IRequest<bool>;
       

}
