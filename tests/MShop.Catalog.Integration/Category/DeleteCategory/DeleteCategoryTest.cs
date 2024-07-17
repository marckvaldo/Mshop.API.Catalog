using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MShop.Calalog.Application.UseCases.Category.DeleteCategory;
using MShop.Catalog.Domain.Respositories;
using MShop.Catalog.Infra.Data.ES.Model;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notification = MShop.Core.Message;

namespace MShop.Catalog.Integration.Category.DeleteCategory
{
    public class DeleteCategoryTest : DeleteCategoryTestFixture, IDisposable
    {
        public DeleteCategoryTest() : base() 
        {
            Deleteindex();
            CreateIndex();
        }

        [Fact(DisplayName = nameof(DeleteCategoryWhenIsValid))]
        [Trait("Integration", "[UserCase] DeleteCategory")]

        public async Task DeleteCategoryWhenIsValid()
        {
            var elastic = serviceProvider.GetRequiredService<IElasticClient>();
            var repository = serviceProvider.GetRequiredService<ICategoryRepository>();
            var mediator = serviceProvider.GetRequiredService<IMediator>();
            var notification = serviceProvider.GetRequiredService<Notification.INotification>();

            var categories = GetListCategories();
            var result = await elastic.IndexManyAsync(categories);
            var input = new DeleteCategoryInput(categories[3].Id);
            var outPut = await mediator.Send(input, CancellationToken.None);

            var resultDeleted = await elastic.GetAsync<CategoryModel>(input.Id);
            var categoryDeleted = resultDeleted.Source;

            Assert.Null(categoryDeleted);
            Assert.False(notification.HasErrors());

        }


        [Fact(DisplayName = nameof(DeleteCategoryWhenIsInValid))]
        [Trait("Integration", "[UserCase] DeleteCategory")]

        public async Task DeleteCategoryWhenIsInValid()
        {
            var elastic = serviceProvider.GetRequiredService<IElasticClient>();
            var repository = serviceProvider.GetRequiredService<ICategoryRepository>();
            var mediator = serviceProvider.GetRequiredService<IMediator>();
            var notification = serviceProvider.GetRequiredService<Notification.INotification>();

            var categories = GetListCategories();
            var result = await elastic.IndexManyAsync(categories);
            var input = new DeleteCategoryInput(Guid.NewGuid());
            var outPut = await mediator.Send(input, CancellationToken.None);

            //var resultDeleted = await elastic.GetAsync<CategoryModel>(input.Id);
            //var categoryDeleted = resultDeleted.Source;

            Assert.False(outPut);
            Assert.True(notification.HasErrors());

        }


        public void Dispose() => DeleteAllDocuments();
    }
}
