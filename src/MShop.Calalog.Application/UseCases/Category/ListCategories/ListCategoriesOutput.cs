using DominaEntity = MShop.Catalog.Domain.Entity;
using MShop.Core.Paginated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MShop.Calalog.Application.UseCases.Category.Common;
using MShop.Calalog.Application.Common;

namespace MShop.Calalog.Application.UseCases.Category.ListCategories
{
    public class ListCategoriesOutput : PaginatedListOutPut<CategoryModelOutPut>
    {
        public ListCategoriesOutput(int currentPage, int perPage, int total, IReadOnlyList<CategoryModelOutPut> itens) : base(currentPage, perPage, total, itens)
        {

        }
    }
}
