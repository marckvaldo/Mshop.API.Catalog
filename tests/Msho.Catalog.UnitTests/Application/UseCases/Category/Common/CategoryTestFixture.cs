using NSubstitute;
using MShop.Calalog.Application.UseCases.Category.CreateCategory;
using MShop.Catalog.Domain.Entity;
using MShop.Catalog.UnitTests.Common;
using BusinessEntity = MShop.Catalog.Domain.Entity;

namespace MShop.Catalog.UnitTests.Application.UseCases.Category.Common
{
    public class CategoryTestFixture : BaseFixture
    {
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


        public static IEnumerable<object[]> ListNamesCategoryInvalid()
        {
            yield return new object[] { InvalidData.GetNameCategoryGreaterThan30CharactersInvalid() };
            yield return new object[] { InvalidData.GetNameCategoryLessThan3CharactersInvalid() };
            yield return new object[] { "" };
            yield return new object[] { null };
        }

        public Product FakerProduct(BusinessEntity.Category category)
        {
            var product = new Product
                (
                    Guid.NewGuid(),
                    faker.Commerce.ProductName(),
                    faker.Commerce.ProductDescription(),
                    Convert.ToDecimal(faker.Commerce.Price()),
                    category.Id,
                    faker.Random.UInt(),
                    true
                );

            return product;
        }

        public BusinessEntity.Category FakerCategory(Guid id)
        {
            var category = new BusinessEntity.Category
                (
                     faker.Commerce.Categories(1)[0],
                     id,
                     true
                );

            return category;
        }

        public List<Product> FakerProducts(int quantity, BusinessEntity.Category category)
        {
            List<Product> listProduct = new List<Product>();
            for (int i = 1; i <= quantity; i++)
                listProduct.Add(FakerProduct(category));

            return listProduct;
        }

        public CreateCategoryInPut FakerRequest()
        {
            return new CreateCategoryInPut(Guid.NewGuid(),faker.Commerce.Categories(1)[0],true);
        }

        public CreateCategoryInPut FakerRequest(string name, bool isActive)
        {
            return new CreateCategoryInPut(Guid.NewGuid(),name,isActive);
        }
    }
}
