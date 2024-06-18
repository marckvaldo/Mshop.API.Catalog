using MShop.Core.DomainObject;
using MShop.Core.Exception;
using MShop.Core.Message;

namespace MShop.Catalog.UnitTests.Domain.Entity.Product
{
    public class ProductTest : ProductTestFixture
    {
        private readonly Notifications _notifications;
        public ProductTest()
        {
            _notifications = new Notifications();
        }

        [Fact(DisplayName = nameof(Instantiate))]
        [Trait("Domain", "Products")]
        public void Instantiate()
        {
            var valid = GetProductValid(Guid.NewGuid());
            var product = GetProductValid(Fake(valid.Id,valid.Description, valid.Name, valid.Price, valid.CategoryId, valid.Stock, valid.IsActive));
            var result = product.IsValid();

            Assert.NotNull(product);
            Assert.True(result.IsValid);
            Assert.Equal(product.Name, valid.Name);
            Assert.Equal(product.Description, valid.Description);
            Assert.Equal(product.Price, valid.Price);
            //Assert.Equal(product.Thumb, valid.Thumb);
            Assert.Equal(product.CategoryId, valid.CategoryId);
            Assert.Equal(product.Stock, valid.Stock);
            Assert.Equal(product.IsActive, valid.IsActive);
            Assert.Equal(product.Id, valid.Id);
            //Assert.Null(product.Thumb);

        }

        [Theory(DisplayName = nameof(SholdReturnErrorWhenDescriptionInvalid))]
        [Trait("Domain", "Products")]
        [MemberData(nameof(ListDescriptionProductInvalid))]

        public void SholdReturnErrorWhenDescriptionInvalid(string description)
        {

            var validade = Fake(
                Guid.NewGuid(),
                description,
                Fake().Name,
                Fake().Price,
                Fake().CategoryId,
                Fake().Stock,
                Fake().IsActive);

            var product = GetProductValid(validade);
            var result = product.IsValid();
           

            Assert.True(result.Errors.Any());
            Assert.False(result.IsValid);

            Assert.Equal(product.Name, validade.Name);
            Assert.Equal(product.Description, description);
            Assert.Equal(product.Price, validade.Price);
            Assert.Equal(product.CategoryId, validade.CategoryId);
            Assert.Equal(product.Stock, validade.Stock);
            Assert.Equal(product.IsActive, validade.IsActive);
            //Assert.Null(product.Thumb);
        }


        [Theory(DisplayName = nameof(SholdReturnErrorWhenNameInvalid))]
        [Trait("Domain", "Products")]
        [MemberData(nameof(ListNameProductInvalid))]
        public void SholdReturnErrorWhenNameInvalid(string name)
        {

            var validade = Fake(
                Guid.NewGuid(),
                Fake().Description,
                name,
                Fake().Price,
                Fake().CategoryId,
                Fake().Stock,
                Fake().IsActive);

            var product = GetProductValid(validade);

            var result = product.IsValid();

            Assert.True(result.Errors.Any());
            Assert.False(result.IsValid);
            Assert.Equal(product.Name, name);
            Assert.Equal(product.Description, validade.Description);
            Assert.Equal(product.Price, validade.Price);
            Assert.Equal(product.CategoryId, validade.CategoryId);
            Assert.Equal(product.Stock, validade.Stock);
            Assert.Equal(product.IsActive, validade.IsActive);
            //Assert.Null(product.Thumb);
        }

        [Theory(DisplayName = nameof(SholdReturnErrorWhenPriceInvalid))]
        [Trait("Domain", "Products")]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(null)]

        public void SholdReturnErrorWhenPriceInvalid(decimal price)
        {

            var validade = Fake(
                Guid.NewGuid(),
                Fake().Description,
                Fake().Name,
                price,
                Fake().CategoryId,
                Fake().Stock,
                Fake().IsActive);

            var product = GetProductValid(validade);

            var result = product.IsValid();

            Assert.True(result.Errors.Any());
            Assert.False(result.IsValid);
            Assert.Equal(product.Name, validade.Name);
            Assert.Equal(product.Description, validade.Description);
            Assert.Equal(product.Price, price);
            Assert.Equal(product.CategoryId, validade.CategoryId);
            Assert.Equal(product.Stock, validade.Stock);
            Assert.Equal(product.IsActive, validade.IsActive);
            //Assert.Null(product.Thumb);

        }


    }
}
