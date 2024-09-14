using MShop.Catalog.UnitTests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntity = MShop.Catalog.Domain.Entity;

namespace MShop.Catalog.UnitTests.Domain.Entity.Product
{
    public class ProductTestFixture: BaseFixture
    {
        private readonly Guid _categoryId;
        private readonly string _categoryName;
        private readonly string _image;

        protected ProductTestFixture()
        {
            _categoryId = Guid.NewGuid();
            _categoryName = faker.Commerce.ProductName()[1..10];
            _image = faker.Image.PicsumUrl();
        }

        protected BusinessEntity.Product GetProductValid(Guid id)
        {
            BusinessEntity.Product produto = new(id, Fake().Description, Fake().Name, Fake().Price, _image);
            produto.AddCategory(new BusinessEntity.Category(_categoryName, _categoryId));
            return produto;
        }
        protected BusinessEntity.Product GetProductValid(ProductFake fake)
        {
            BusinessEntity.Product produto = new(fake.Id, fake.Description, fake.Name, fake.Price, _image, fake.Stock);
            produto.AddCategory(new BusinessEntity.Category(_categoryName, _categoryId));
            return produto;
        }

        protected ProductFake Fake()
        {
            return new ProductFake
            {
                Name = faker.Commerce.ProductName(),
                Description = faker.Commerce.ProductDescription(),
                Price = Convert.ToDecimal(faker.Commerce.Price()),
                CategoryId = _categoryId,
                Stock = faker.Random.UInt(),
                CategoryName = _categoryName
            };
        }

        protected ProductFake FakeImage()
        {
            var product = new ProductFake
            {
                Name = faker.Commerce.ProductName(),
                Description = faker.Commerce.ProductDescription(),
                Price = Convert.ToDecimal(faker.Commerce.Price()),
                CategoryId = _categoryId,
                Stock = faker.Random.UInt(),
            };

            return product;
        }

        protected ProductFake Fake(Guid id,string description, string name, decimal price, Guid categoryId, decimal stock)
        {
            return new ProductFake
            {
                Id = id,
                Description = description,
                Name = name,
                Price = price,
                CategoryId = categoryId,
                Stock = stock,
            };
        }

          public static IEnumerable<object[]> ListNameProductInvalid()
        {
            yield return new object[] { InvalidData.GetNameProductGreaterThan255CharactersInvalid() };
            yield return new object[] { InvalidData.GetNameProductLessThan3CharactersInvalid() };
            yield return new object[] { "" };
            yield return new object[] { " " };
            yield return new object[] { null };
        }

        public static IEnumerable<object[]> ListDescriptionProductInvalid()
        {
            //yield return new object[] { InvalidData.GetDescriptionProductGreaterThan1000CharactersInvalid() };
            //yield return new object[] { InvalidData.GetDescriptionProductLessThan10CharactersInvalid() };
            yield return new object[] { "" };
            yield return new object[] { " " };
            yield return new object[] { null };
        }
    }

    public class ProductFake
    {
        public Guid Id { get;set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public FileImage? Thumb { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Stock { get; set; }
        public string CategoryName { get; set; }
    }

}
