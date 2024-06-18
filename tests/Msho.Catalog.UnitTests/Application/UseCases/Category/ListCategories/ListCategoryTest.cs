using MShop.Calalog.Application.UseCases.Category.ListCategories;
using MShop.Catalog.Domain.Respositories;
using MShop.Core.Enum.Paginated;
using MShop.Core.Paginated;
using NSubstitute;
using ApplicationUseCases = MShop.Calalog.Application.UseCases.Category.ListCategories;
using BusinessEntity = MShop.Catalog.Domain.Entity;

namespace MShop.Catalog.UnitTests.Application.UseCases.Category.ListCategories
{
    public class ListCategoryTest : ListCategoryFixtureTest
    {
        [Fact(DisplayName = nameof(ListCategory))]
        [Trait("Application-UseCase", "List Categogry")]

        public async void ListCategory()
        {
            var repository = Substitute.For<ICategoryRepository>();
           
            var categorys = FakerCategorys(10);

            var result = new PaginatedOutPut<BusinessEntity.Category>(1, 10, 10, categorys);

            var request = new ListCategoryInput(1, 10, "", "Name", SearchOrder.Desc);

            repository.FilterPaginate(Arg.Any<PaginatedInPut>(), CancellationToken.None).Returns(result);

            var useCase = new ApplicationUseCases.ListCategories(repository);
            var outPut = await useCase.Handle(request, CancellationToken.None);

            Assert.NotNull(outPut);
            Assert.NotNull(outPut.Itens);
            Assert.Equal(outPut.PerPage, request.PerPage);
            Assert.Equal(10, outPut.Total);
            Assert.Equal(outPut.Page, request.Page);
            Assert.Equal(10, outPut.Itens.Count);


        }
    }
}
