using MShop.Catalog.Infra.Data.ES.Repositoty;
using MShop.Catalog.UnitTests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using MShop.Core.Data;
using MShop.Catalog.Domain.Respositories;
using BusinessEntity = MShop.Catalog.Domain.Entity;

namespace MShop.Catalog.UnitTests.Infra.Repository.Common
{
    public class RepositoryTestFixture : BaseFixture
    {
        public RepositoryTestFixture() : base()
        {

        }

        public BusinessEntity.Category Faker(Guid id)
        {
            return new BusinessEntity.Category(faker.Commerce.Categories(1)[0], id);
        }

        public List<BusinessEntity.Category> FakerCategorys(int quantity)
        {
            List<BusinessEntity.Category> listCategory = new List<BusinessEntity.Category>();
            for (int i = 1; i <= quantity; i++)
                listCategory.Add(Faker(Guid.NewGuid()));

            return listCategory;
        }


    }
}
