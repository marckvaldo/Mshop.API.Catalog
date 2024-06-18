using MShop.Catalog.Domain.Respositories;
using MShop.Core.Message;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using BusinessEntity = MShop.Catalog.Domain.Entity;
using useCase = MShop.Calalog.Application.UseCases.Category.CreateCategory;

namespace MShop.Catalog.UnitTests.Application.UseCases.Category.CreateCategory
{
    public class CategoryTest : CreateCategoryTestFixture
    {
        [Fact(DisplayName = nameof(CreateCategory))]
        [Trait("Application-UseCase", "Create Category")]

        public async void CreateCategory()
        {
            var repository = Substitute.For<ICategoryRepository>();
            var notification = Substitute.For<INotification>();

            var request = FakerRequest();

            var useCase = new useCase.CreateCategory(repository, notification);
            var outPut = await useCase.Handle(request, CancellationToken.None);

            await repository.Received(1).Create(Arg.Any<BusinessEntity.Category>(), CancellationToken.None);
            notification.Received(0).AddNotifications(Arg.Any<string>());
           

            Assert.True(outPut);

        }


        [Theory(DisplayName = nameof(ShouldReturnErrorWhenCreateCategoryInvalid))]
        [Trait("Application-UseCase", "Create Category")]
        [MemberData(nameof(ListNamesCategoryInvalid))]

        public async void ShouldReturnErrorWhenCreateCategoryInvalid(string name)
        {
            var repository = Substitute.For<ICategoryRepository>();
            var notification = Substitute.For<INotification>();

            var request = FakerRequest(name, true);

            var useCase = new useCase.CreateCategory(repository, notification);
            var outPut = await useCase.Handle(request, CancellationToken.None);

            //var exception = Assert.ThrowsAsync<EntityValidationException>(action);
            await repository.Received(0).Create(Arg.Any<BusinessEntity.Category>(), CancellationToken.None);      
            notification.Received().AddNotifications(Arg.Any<string>());
        }
    }
}
