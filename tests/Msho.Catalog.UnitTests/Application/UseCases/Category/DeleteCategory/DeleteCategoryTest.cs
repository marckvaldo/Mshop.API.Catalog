using MShop.Catalog.Domain.Respositories;
using MShop.Core.Message;
using NSubstitute;
using BusinessEntity = MShop.Catalog.Domain.Entity;
using useCase = MShop.Calalog.Application.UseCases.Category.DeleteCategory;

namespace MShop.Catalog.UnitTests.Application.UseCases.Category.DeleteCategory
{
    public class DeleteCategoryTest : DeleteCategoryTestFixture
    {
        [Fact(DisplayName = nameof(DeleteCategory))]
        [Trait("Application-UseCase", "Delete Category")]

        public async void DeleteCategory()
        {
            var repository = Substitute.For<ICategoryRepository>();
            var notification = Substitute.For<INotification>();
            var repositoryProduct = Substitute.For<IProductRepository>();

            var category = Faker(Guid.NewGuid());

            //repositoryProduct.Setup(r => r.GetProductsByCategoryId(Arg.Any<Guid>())).ReturnsAsync(FakerProducts(6,FakerCategory()));
            repository.GetById(Arg.Any<Guid>()).Returns(category);

            var useCase = new useCase.DeleteCategory(
                repository, 
                repositoryProduct, 
                notification);

            var outPut = await useCase.Handle(new useCase.DeleteCategoryInput(category.Id), CancellationToken.None);

            await repository.Received(1).GetById(Arg.Any<Guid>());
            notification.Received(0).AddNotifications(Arg.Any<string>());         

            Assert.True(outPut);
        }



        [Fact(DisplayName = nameof(ShouldReturnErroWhenDeleteCategoryNotFound))]
        [Trait("Application-UseCase", "Delete Category")]

        public async void ShouldReturnErroWhenDeleteCategoryNotFound()
        {
            var repository = Substitute.For<ICategoryRepository>();
            var notification = Substitute.For<INotification>();
            var repositoryProduct = Substitute.For<IProductRepository>();

            var category = Faker(Guid.NewGuid());

            var useCase = new useCase.DeleteCategory(
                repository, 
                repositoryProduct, 
                notification);

            var outPut = await useCase.Handle(new useCase.DeleteCategoryInput(category.Id), CancellationToken.None);

            //var exception = Assert.ThrowsAsync<ApplicationValidationException>(action);
            await repository.Received(0).DeleteById(Arg.Any<BusinessEntity.Category>(), CancellationToken.None);         
            notification.Received(1).AddNotifications(Arg.Any<string>());
            await repositoryProduct.Received(0).GetProductsByCategoryId(Arg.Any<Guid>());

            Assert.False(outPut);

        }



        [Fact(DisplayName = nameof(ShouldReturnErroWhenDeleteCategoryWhenThereAreSameProducts))]
        [Trait("Application-UseCase", "Delete Category")]

        public async void ShouldReturnErroWhenDeleteCategoryWhenThereAreSameProducts()
        {
            var repository = Substitute.For<ICategoryRepository>();
            var repositoryProduct = Substitute.For<IProductRepository>();
            var notification = Substitute.For<INotification>();
          

            var category = Faker(Guid.NewGuid());
            var product = FakerProducts(3, category);

            repository.GetById(Arg.Any<Guid>()).Returns(category);
            repositoryProduct.GetProductsByCategoryId(Arg.Any<Guid>()).Returns(product);
            

            var useCase = new useCase.DeleteCategory(
                repository, 
                repositoryProduct, 
                notification);

            var outPut = await useCase.Handle(new useCase.DeleteCategoryInput(category.Id), CancellationToken.None);

            //var exception = Assert.ThrowsAsync<ApplicationValidationException>(action);
            await repository.Received(1).GetById(Arg.Any<Guid>());
            await repository.Received(0).DeleteById(Arg.Any<BusinessEntity.Category>(), CancellationToken.None);
            notification.Received(1).AddNotifications(Arg.Any<string>());

            Assert.False(outPut);

        }
    }
}
