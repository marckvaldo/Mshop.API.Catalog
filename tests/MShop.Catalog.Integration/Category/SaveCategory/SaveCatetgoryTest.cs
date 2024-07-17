using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MShop.Calalog.Application.UseCases.Category.CreateCategory;
using MShop.Catalog.Domain.Respositories;
using MShop.Catalog.Infra.Data.ES.Model;
using MShop.Core.Message;
using Nest;
using Notification = MShop.Core.Message;

namespace MShop.Catalog.Integration.Category.SaveCategory
{
    public class SaveCatetgoryTest : SaveCatetgoryTestFixture, IDisposable
    {
        //private Notification.INotification _notification;
        
        public SaveCatetgoryTest() : base()
        {
            //_notification = new Notification.Notifications();
            Deleteindex();
            CreateIndex();
        }

        [Fact(DisplayName = nameof(SaveCategoryWhenInputIsValid))]
        [Trait("Integration", "[UserCase] CreateCategory")]
        public async Task SaveCategoryWhenInputIsValid()
        {

            var elastic = serviceProvider.GetRequiredService<IElasticClient>();
            var Repository = serviceProvider.GetRequiredService<ICategoryRepository>();
            var mediator = serviceProvider.GetRequiredService<IMediator>();
            var notification = serviceProvider.GetRequiredService<Notification.INotification>();
            //var Repository = new CategoryRepository(elastic);


            var Input = GetCategoryValid();


            //var UserCase = new CreateCategory(Repository,_notification);
            //var outPut = await UserCase.Handle(Input, CancellationToken.None);
            var outPut = await mediator.Send(Input);
            Assert.True(outPut);

            var result = await elastic.GetAsync<CategoryModel>(Input.Id);
            var documento = result.Source;
            Assert.NotNull(documento);
            Assert.True(result.Found);
            Assert.Equal(documento.Name, Input.Name);
            Assert.Equal(documento.Id, Input.Id);
            Assert.Equal(documento.IsActive, Input.IsActive);
            Assert.False(notification.HasErrors());
            //Assert.False(_notification.HasErrors());

        }

        [Fact(DisplayName = nameof(SaveCategoryWhenInputIsInValid))]
        [Trait("Integration", "[UserCase] CreateCategory")]
        public async Task SaveCategoryWhenInputIsInValid()
        {

            var elastic = serviceProvider.GetRequiredService<IElasticClient>();
            var Repository = serviceProvider.GetRequiredService<ICategoryRepository>();
            var notification = serviceProvider.GetRequiredService<Notification.INotification>();
            var mediator = serviceProvider.GetRequiredService<IMediator>();

            //var Repository = new CategoryRepository(elastic);

            var Input = GetCategoryValid();
            Input.Name = null;

            //var UserCase = new CreateCategory(Repository, _notification);
            //var outPut = await UserCase.Handle(Input, CancellationToken.None);

            var outPut = await mediator.Send(Input, CancellationToken.None);

            var result = await elastic.GetAsync<CategoryModel>(Input.Id);
            var documento = result.Source;
            Assert.Null(documento);

            Assert.False(outPut);
            Assert.True(notification.HasErrors());
            Assert.Single(notification.Errors());

            //Assert.True(_notification.HasErrors());
            //Assert.Single(_notification.Errors());

        }

        public void Dispose()  => DeleteAllDocuments();

    }
}
