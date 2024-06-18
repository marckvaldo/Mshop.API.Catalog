using Moq;
using BusinessEntity = MShop.Catalog.Domain.Entity;
using MShop.Catalog.Domain.Respositories;
using MShop.Core.Data;
using MShop.Core.Exception;
using MShop.Core.Message;
using useCase = MShop.Calalog.Application.UseCases.Category.CreateCategory;

namespace MShop.Catalog.UnitTests.Application.UseCases.Category.CreateCategory
{
    public class CategoryTest : CreateCategoryTestFixture
    {
        [Fact(DisplayName = nameof(CreateCategory))]
        [Trait("Application-UseCase", "Create Category")]

        public async void CreateCategory()
        {
            var repository = new Mock<ICategoryRepository>();
            var notification = new Mock<INotification>();

            var request = FakerRequest();

            var useCase = new useCase.CreateCategory(repository.Object, notification.Object);
            var outPut = await useCase.Handle(request, CancellationToken.None);

            repository.Verify(r => r.Create(It.IsAny<BusinessEntity.Category>(), CancellationToken.None), Times.Once);
            notification.Verify(n => n.AddNotifications(It.IsAny<string>()), Times.Never);
           

            Assert.True(outPut);

        }


        [Theory(DisplayName = nameof(ShouldReturnErrorWhenCreateCategoryInvalid))]
        [Trait("Application-UseCase", "Create Category")]
        [MemberData(nameof(ListNamesCategoryInvalid))]

        public async void ShouldReturnErrorWhenCreateCategoryInvalid(string name)
        {
            var repository = new Mock<ICategoryRepository>();
            var notification = new Mock<INotification>();

            var request = FakerRequest(name, true);

            var useCase = new useCase.CreateCategory(repository.Object, notification.Object);
            var outPut = await useCase.Handle(request, CancellationToken.None);

            //var exception = Assert.ThrowsAsync<EntityValidationException>(action);
            repository.Verify(n => n.Create(It.IsAny<BusinessEntity.Category>(), CancellationToken.None), Times.Never);
            notification.Verify(n => n.AddNotifications(It.IsAny<string>()), Times.AtMost(2)) ;
        }
    }
}
