﻿using Microsoft.VisualStudio.TestPlatform.Utilities;
using Moq;
using MShop.Catalog.Domain.Respositories;
using MShop.Core.Data;
using MShop.Core.Exception;
using MShop.Core.Message;
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
            var repository = new Mock<ICategoryRepository>();
            var notification = new Mock<INotification>();
            var repositoryProduct = new Mock<IProductRepository>();

            var category = Faker(Guid.NewGuid());

            //repositoryProduct.Setup(r => r.GetProductsByCategoryId(It.IsAny<Guid>())).ReturnsAsync(FakerProducts(6,FakerCategory()));
            repository.Setup(r => r.GetById(It.IsAny<Guid>())).ReturnsAsync(category);

            var useCase = new useCase.DeleteCategory(
                repository.Object, 
                repositoryProduct.Object, 
                notification.Object);

            var outPut = await useCase.Handle(new useCase.DeleteCategoryInput(category.Id), CancellationToken.None);

            repository.Verify(r => r.GetById(It.IsAny<Guid>()), Times.Once);
            notification.Verify(n => n.AddNotifications(It.IsAny<string>()), Times.Never);            

            Assert.True(outPut);
        }



        [Fact(DisplayName = nameof(ShouldReturnErroWhenDeleteCategoryNotFound))]
        [Trait("Application-UseCase", "Delete Category")]

        public async void ShouldReturnErroWhenDeleteCategoryNotFound()
        {
            var repository = new Mock<ICategoryRepository>();
            var notification = new Mock<INotification>();
            var repositoryProduct = new Mock<IProductRepository>();

            var category = Faker(Guid.NewGuid());

            var useCase = new useCase.DeleteCategory(
                repository.Object, 
                repositoryProduct.Object, 
                notification.Object);

            var outPut = await useCase.Handle(new useCase.DeleteCategoryInput(category.Id), CancellationToken.None);

            //var exception = Assert.ThrowsAsync<ApplicationValidationException>(action);
            repository.Verify(n => n.DeleteById(It.IsAny<BusinessEntity.Category>(), CancellationToken.None), Times.Never);          
            notification.Verify(n => n.AddNotifications(It.IsAny<string>()), Times.Once);
            repositoryProduct.Verify(n => n.GetProductsByCategoryId(It.IsAny<Guid>()), Times.Never);

            Assert.False(outPut);

        }



        [Fact(DisplayName = nameof(ShouldReturnErroWhenDeleteCategoryWhenThereAreSameProducts))]
        [Trait("Application-UseCase", "Delete Category")]

        public async void ShouldReturnErroWhenDeleteCategoryWhenThereAreSameProducts()
        {
            var repository = new Mock<ICategoryRepository>();
            var repositoryProduct = new Mock<IProductRepository>();
            var notification = new Mock<INotification>();
          

            var category = Faker(Guid.NewGuid());
            var product = FakerProducts(3, category);

            repository.Setup(r => r.GetById(It.IsAny<Guid>())).ReturnsAsync(category);
            repositoryProduct.Setup(r => r.GetProductsByCategoryId(It.IsAny<Guid>())).ReturnsAsync(product);
            

            var useCase = new useCase.DeleteCategory(
                repository.Object, 
                repositoryProduct.Object, 
                notification.Object);

            var outPut = await useCase.Handle(new useCase.DeleteCategoryInput(category.Id), CancellationToken.None);

            //var exception = Assert.ThrowsAsync<ApplicationValidationException>(action);
            repository.Verify(n => n.GetById(It.IsAny<Guid>()), Times.Once);
            repository.Verify(n => n.DeleteById(It.IsAny<BusinessEntity.Category>(), CancellationToken.None), Times.Never);
            notification.Verify(n => n.AddNotifications(It.IsAny<string>()), Times.Once);

            Assert.False(outPut);

        }
    }
}