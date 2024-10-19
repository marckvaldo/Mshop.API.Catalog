using MShop.Calalog.Application.UseCases.Products.ListProduct;
using MShop.Catalog.Domain.Respositories;
using MShop.Core.Enum.Paginated;
using MShop.Core.Message;
using MShop.Core.Paginated;
using NSubstitute;
using Entity = MShop.Catalog.Domain.Entity;
using useCaseListProduct = MShop.Calalog.Application.UseCases.Products.ListProduct;

namespace MShop.Catalog.UnitTests.Application.UseCases.Product.ListProduct
{
    public class ListProductTeste : ListProductTesteFixture
    {
        public ListProductTeste() : base()
        {
        }

        [Fact(DisplayName = nameof(ListProducts))]
        [Trait("Application-UseCase", "List Product")]
        public async void ListProducts()
        {
            var repository = Substitute.For<IProductRepository>();
            var notification = Substitute.For<INotification>();

            var productsFake = RequestListProducts(20);

            var random = new Random();

            var request = new ListProductInput(
                page: random.Next(1, 10),
                perPage: random.Next(10, 20),
                search: "teste",
                orderBy: "name",
                order: SearchOrder.Asc);

            var outPutRespository = new PaginatedOutPut<Entity.Product>(
                currentPage: request.Page,
                perPage: request.PerPage,
                total: productsFake.Count,
                productsFake); ;

            repository.FilterPaginate(request, CancellationToken.None).Returns(Task.FromResult(outPutRespository));

            var useCase = new useCaseListProduct.ListProduct(notification, repository);
            var outPut = await useCase.Handle(request, CancellationToken.None);

            await repository.Received(1).FilterPaginate(request, CancellationToken.None);
            notification.Received(0).AddNotifications(Arg.Any<string>());

            Assert.Equal(outPut.Total, productsFake.Count);
            Assert.Equal(request.PerPage, outPut.PerPage);
            Assert.Equal(request.Page, outPut.Page);
            Assert.NotNull(outPut.Itens);
        }
    }
}
