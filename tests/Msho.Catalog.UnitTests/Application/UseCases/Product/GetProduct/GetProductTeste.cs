using MShop.Catalog.Domain.Respositories;
using MShop.Core.Message;
using NSubstitute;
using useCaseGetProduct = MShop.Calalog.Application.UseCases.Products.GetProduct;

namespace MShop.Catalog.UnitTests.Application.UseCases.Product.GetProduct
{
    public class GetProductTeste : GetProductTesteFixture
    {
        public GetProductTeste() : base()
        {

        }

        [Fact(DisplayName = nameof(GetProduct))]
        [Trait("Application-UseCase", "Get Product")]
        public async void GetProduct()
        {
            var repository = Substitute.For<IProductRepository>();
            var notification = Substitute.For<INotification>();

            var input = RequestCreateProdutcNomal(Guid.NewGuid(), Guid.NewGuid());

            repository.GetById(Arg.Any<Guid>()).Returns(Task.FromResult(CreateProdutoInputToProduct(input)));

            var useCase = new useCaseGetProduct.GetProduct(notification, repository);
            var outPut = await useCase.Handle(new useCaseGetProduct.GetProductInput(input.Id), CancellationToken.None);

            await repository.Received(1).GetById(Arg.Any<Guid>());
            notification.Received(0).AddNotifications(Arg.Any<string>());

            Assert.Equal(outPut.Name, input.Name);
            Assert.Equal(outPut.Price, input.Price);
            Assert.Equal(outPut.IsSale, input.IsSale);
            Assert.Equal(outPut.Category, input.Category);
            Assert.Equal(outPut.Description, input.Description);
            Assert.Equal(outPut.Stock, input.Stock);
            Assert.Equal(outPut.CategoryId, input.CategoryId);

        }


        [Fact(DisplayName = nameof(ShouldErrorWhenGetProduct))]
        [Trait("Application-UseCase", "Get Product")]
        public async void ShouldErrorWhenGetProduct()
        {
            var repository = Substitute.For<IProductRepository>();
            var notification = Substitute.For<INotification>();

            var input = RequestCreateProdutcNomal(Guid.NewGuid(), Guid.NewGuid());

            var useCase = new useCaseGetProduct.GetProduct(notification, repository);
            var outPut = await useCase.Handle(new useCaseGetProduct.GetProductInput(input.Id), CancellationToken.None);

            await repository.Received(1).GetById(Arg.Any<Guid>());
            notification.Received(1).AddNotifications(Arg.Any<string>());
        }
    }
}
