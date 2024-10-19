using MShop.Calalog.Application.UseCases.Products.DeleteProduct;
using MShop.Catalog.Domain.Respositories;
using MShop.Catalog.UnitTests.Application.UseCases.Product.Common;
using MShop.Core.Message;
using NSubstitute;

using Entity = MShop.Catalog.Domain.Entity;
using useCaseDeleteProduct = MShop.Calalog.Application.UseCases.Products.DeleteProduct;



namespace MShop.Catalog.UnitTests.Application.UseCases.Product.DeleteProduct
{
    public class DeleteProductTeste : ProductTesteFixture
    {
        public DeleteProductTeste() : base() { }
        

        [Fact(DisplayName = nameof(DeleteProduct))]
        [Trait("Application-UseCase", "Delete Product")]
        public async void DeleteProduct()
        {
            var repository = Substitute.For<IProductRepository>();
            var notification = Substitute.For<INotification>();

            var input = RequestCreateProdutcNomal(Guid.NewGuid(), Guid.NewGuid());
            repository.GetById(Arg.Any<Guid>())
                .Returns(Task.FromResult(CreateProdutoInputToProduct(input)));

            //repository.DeleteById(Arg.Any<Guid>()).Returns(Task.FromResult());

            var useCase = new useCaseDeleteProduct.DeleteProduct(notification, repository);
            var outPut = await useCase.Handle(new DeleteProductInput(input.Id), CancellationToken.None);

            await repository.Received(1).GetById(Arg.Any<Guid>());
            await repository.Received(1).DeleteById(Arg.Any<Entity.Product>(), CancellationToken.None);
            notification.Received(0).AddNotifications(Arg.Any<string>());

            Assert.True(outPut);
        }

        [Fact(DisplayName = nameof(ShouldReturnErrorWhenDeleteProductAsync))]
        [Trait("Application-UseCase", "Delete Product")]
        public async Task ShouldReturnErrorWhenDeleteProductAsync()
        {
            var repository = Substitute.For<IProductRepository>();
            var notification = Substitute.For<INotification>();

            var input = RequestCreateProdutcNomal(Guid.NewGuid(), Guid.NewGuid());


            //repository.DeleteById(Arg.Any<Guid>()).Returns(Task.FromResult());

            var useCase = new useCaseDeleteProduct.DeleteProduct(notification, repository);
            var outPut = await useCase.Handle(new DeleteProductInput(Guid.NewGuid()), CancellationToken.None);

            await repository.Received(1).GetById(Arg.Any<Guid>());
            await repository.Received(0).DeleteById(Arg.Any<Entity.Product>(), CancellationToken.None);
            notification.Received(1).AddNotifications(Arg.Any<string>());

            Assert.False(outPut);
        }

    }
}
