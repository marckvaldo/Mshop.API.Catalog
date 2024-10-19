using Bogus;
using MShop.Catalog.UnitTests.Common;
using MShop.Core.DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using useCase = MShop.Calalog.Application.UseCases.Products;
using Entity = MShop.Catalog.Domain.Entity;

namespace MShop.Catalog.UnitTests.Application.UseCases.Product.Common
{
    public class ProductTesteFixture : BaseFixture
    {
        public ProductTesteFixture() : base()
        {
        }

        protected useCase.CreateProduct.CreateProductInput RequestCreateProdutcNomal(Guid id, Guid idCategory)
        {
            return new useCase.CreateProduct.CreateProductInput(
            id,
                faker.Commerce.ProductDescription(),
                faker.Name.FullName(),
                decimal.Parse(faker.Commerce.Price()),
                10,
                false,
                idCategory,
                faker.Image.PlaceImgUrl(),
                faker.Commerce.Categories(1).First());
        }

        protected useCase.CreateProduct.CreateProductInput RequestCreateProdutcInvalid(Guid id, Guid idCategory)
        {
            return new useCase.CreateProduct.CreateProductInput(
            id,
                faker.Commerce.ProductDescription(),
                faker.Name.FullName()[0..2],
                decimal.Parse(faker.Commerce.Price()),
                0,
                false,
                idCategory,
                faker.Image.PlaceImgUrl(),
                faker.Commerce.Categories(1).First());
        }

        protected Entity.Product CreateProdutoInputToProduct(useCase.CreateProduct.CreateProductInput createProduct)
        {
            var product = new Entity.Product(createProduct.Id, createProduct.Description, createProduct.Name, createProduct.Price, createProduct.Thumb, createProduct.Stock);
            product.AddCategory(new Entity.Category(createProduct.Category, createProduct.CategoryId, true));
            return product;
        }

        protected List<Entity.Product> RequestListProducts(int limit = 10)
        {
            var products = new List<Entity.Product>();

            for (int i = 0; i <= limit; i++)
            {
                var product = new Entity.Product(
                    Guid.NewGuid(),
                    faker.Commerce.ProductDescription(),
                    faker.Commerce.ProductName(),
                    decimal.Parse(faker.Commerce.Price()),
                    faker.Image.PlaceImgUrl(),
                    0);
                product.AddCategory(new Entity.Category(faker.Commerce.Categories(1).ToString(), Guid.NewGuid(), true));

                products.Add(product);
            }
            return products;
        }


    }
}
