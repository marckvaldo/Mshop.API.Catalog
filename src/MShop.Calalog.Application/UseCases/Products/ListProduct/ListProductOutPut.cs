using MShop.Calalog.Application.Common;
using MShop.Calalog.Application.UseCases.Products.Commun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MShop.Calalog.Application.UseCases.Products.ListProduct
{
    public class ListProductOutPut : PaginatedListOutPut<ProductOutPut>
    {
        public ListProductOutPut(int page, int perPage, int total, IReadOnlyList<ProductOutPut> itens) : base(page, perPage, total, itens)
        {
        }
    }
}
