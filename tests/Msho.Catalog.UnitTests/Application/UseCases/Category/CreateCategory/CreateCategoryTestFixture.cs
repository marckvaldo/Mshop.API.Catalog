using MShop.Calalog.Application.UseCases.Category.CreateCategory;
using MShop.Catalog.UnitTests.Application.UseCases.Category.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MShop.Catalog.UnitTests.Application.UseCases.Category.CreateCategory
{
    public class CreateCategoryTestFixture : CategoryTestFixture
    {
        public CreateCategoryInPut FakerRequest()
        {
            return new CreateCategoryInPut(Guid.NewGuid(),faker.Commerce.Categories(1)[0], true);
        }

        public CreateCategoryInPut FakerRequest(string name, bool isActive)
        {
            return new CreateCategoryInPut(Guid.NewGuid(),name,isActive);
        }
    }
}
