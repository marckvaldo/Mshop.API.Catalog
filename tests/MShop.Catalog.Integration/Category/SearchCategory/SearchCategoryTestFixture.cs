using MShop.Catalog.Infra.Data.ES.Model;
using MShop.Catalog.Integration.Category.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MShop.Catalog.Integration.Category.SearchCategory
{
    public class SearchCategoryTestFixture : BaseFixtureCategory
    {
        public SearchCategoryTestFixture() : base()
        {

        }

        protected IList<CategoryModel> GetListCategories(IEnumerable<string> categoryName)
        {
            return categoryName.Select(x => new CategoryModel { Id = Guid.NewGuid(), Name = x, IsActive = true }).ToList();
        }

        protected IList<CategoryModel> GetCloneListCategory(IList<CategoryModel> categories, string orderBy, Core.Enum.Paginated.SearchOrder order)
        {
            var listClone = new List<CategoryModel>(categories);
            switch (orderBy.ToLower(), order) {
                case ("name", Core.Enum.Paginated.SearchOrder.Desc):
                    listClone = listClone.OrderByDescending(x => x.Name).ToList();
                    break;
                case ("name",Core.Enum.Paginated.SearchOrder.Asc):
                    listClone = listClone.OrderBy(x => x.Name).ToList();
                    break;
                case ("id", Core.Enum.Paginated.SearchOrder.Asc):
                    listClone = listClone.OrderBy(x=>x.Id).ToList();
                    break;
                case ("id", Core.Enum.Paginated.SearchOrder.Desc):
                    listClone = listClone.OrderByDescending(x=>x.Id).ToList();
                    break;
                default:
                    listClone = listClone.OrderBy(x => x.Name).ToList();
                    break;
            }

            return listClone;
        }
    }
}
