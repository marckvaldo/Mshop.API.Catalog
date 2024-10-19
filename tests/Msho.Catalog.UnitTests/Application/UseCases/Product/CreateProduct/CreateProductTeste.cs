using MShop.Catalog.Domain.Respositories;
using MShop.Core.Message;
using NSubstitute;
using Entity = MShop.Catalog.Domain.Entity;
using useCaseCreateProduct = MShop.Calalog.Application.UseCases.Products.CreateProduct;


namespace MShop.Catalog.UnitTests.Application.UseCases.Product.CreateProduct
{
    public class CreateProductTeste : CreateProdutTestFixture
    {
        public CreateProductTeste() : base()
        {
        }

        [Fact(DisplayName = nameof(CreateProduct))]
        [Trait("Application-UseCase", "Create Product")]

        public async void CreateProduct()
        {
            var repository = Substitute.For<IProductRepository>();
            repository.Create(Arg.Any<Entity.Product>(), CancellationToken.None).Returns(Task.FromResult(true));
            var notification = Substitute.For<INotification>();

            var input = RequestCreateProdutcNomal(Guid.NewGuid(), Guid.NewGuid());

            var useCase = new useCaseCreateProduct.CreateProduct(notification, repository);
            var outPut = await useCase.Handle(input, CancellationToken.None);

            await repository.Received(1).Create(Arg.Any<Entity.Product>(), CancellationToken.None);
            notification.Received(0).AddNotifications(Arg.Any<string>());

            Assert.True(outPut);

        }

        [Fact(DisplayName = nameof(ShouldErrorWhenCreateInvalidProduct))]
        [Trait("Application-UseCase", "Create Product")]
        public async void ShouldErrorWhenCreateInvalidProduct()
        {
            var repository = Substitute.For<IProductRepository>();
            repository.Create(Arg.Any<Entity.Product>(), CancellationToken.None).Returns(Task.FromResult(false));

            var notifications = Substitute.For<INotification>();

            var input = RequestCreateProdutcInvalid(Guid.NewGuid(), Guid.NewGuid());
            
            var useCase = new useCaseCreateProduct.CreateProduct(notifications, repository);
            var outPut = await useCase.Handle(input, CancellationToken.None);

            await repository.Received(0).Create(Arg.Any<Entity.Product>(), CancellationToken.None);
            notifications.Received(1).AddNotifications(Arg.Any<string>());

            Assert.False(outPut);
        }

        
      
        
    }
}
