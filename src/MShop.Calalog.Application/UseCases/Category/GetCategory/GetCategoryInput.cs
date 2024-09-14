using MediatR;
using MShop.Calalog.Application.UseCases.Category.Common;
using MShop.Catalog.Domain.Respositories;
using MShop.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MShop.Calalog.Application.UseCases.Category.GetCategory
{
    public class GetCategoryInput : IRequest<CategoryModelOutPut>
    {
        public GetCategoryInput(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
