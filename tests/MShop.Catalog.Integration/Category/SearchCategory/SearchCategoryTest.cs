using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StartIndex = MShop.Catalog.Infra.Data.ES.StartIndex;
using Nest;
using MShop.Calalog.Application.UseCases.Category.ListCategories;

namespace MShop.Catalog.Integration.Category.SearchCategory
{
    public class SearchCategoryTest : SearchCategoryTestFixture, IDisposable
    {
        public SearchCategoryTest(): base()
        {
            Deleteindex();
            CreateIndex();
        }

        [Theory(DisplayName = nameof(SearchCategory_WhereReceivesValidSearchInput_ReturnFilteredList))]
        [Trait("Integration", "[UserCase] SearchCategory")]
        [InlineData("Action",5,1,1,1)]
        [InlineData("Horror", 5, 1, 3, 3)]
        [InlineData("Horror", 5, 2, 0, 3)]
        [InlineData("Sci-fi", 5, 1, 4, 4)]
        [InlineData("Sci-fi", 2, 1, 2, 4)]
        [InlineData("Sci-fi", 3, 2, 1, 4)]
        [InlineData("Others", 5, 1, 0, 0)]
        [InlineData("Robots", 5, 1, 2, 2)]
        public async Task SearchCategory_WhereReceivesValidSearchInput_ReturnFilteredList(
            string serarch, 
            int perPage,
            int page,
            int expertedItemsCount,
            int expertedTotalCount)
        {
            var elastic = serviceProvider.GetRequiredService<IElasticClient>();
            var mediator = serviceProvider.GetRequiredService<IMediator>();

            var categoryNameList = new List<string>()
            {
                "Action",
                "Horror",
                "Horror - Robots",
                "Horror - Based on Real Facts",
                "Drama",
                "Sci-fi IA",
                "Sci-fi Space",
                "Sci-fi Robots",
                "Sci-fi Future",
            };

            var exemples = GetListCategories(categoryNameList);

            await elastic.IndexManyAsync(exemples);
            await elastic.Indices.RefreshAsync(StartIndex.IndexName.Category);

            var inPut = new ListCategoryInput(page, perPage, serarch);

            var outPut = await mediator.Send(inPut);

            Assert.NotNull(outPut);
            Assert.NotNull(outPut.Itens);
            Assert.True(outPut.Page == page);
            Assert.True(outPut.PerPage == perPage);
            Assert.True(outPut.Itens.Count() == expertedItemsCount);
            Assert.True(outPut.Total == expertedTotalCount);

            foreach (var item in outPut.Itens)
            {
                Assert.True(item.Name == exemples.First(x => x.Id == item.Id).Name);
                Assert.True(item.IsActive == exemples.First(x => x.Id == item.Id).IsActive);
            }

        }

        [Theory(DisplayName = nameof(SearchCategory_WhereReceivesValidSearchInput_ReturnOrderList))]
        [Trait("Integration", "[UserCase] SearchCategory")]
        [InlineData("name",Core.Enum.Paginated.SearchOrder.Asc)]
        [InlineData("name", Core.Enum.Paginated.SearchOrder.Desc)]
        [InlineData("id", Core.Enum.Paginated.SearchOrder.Asc)]
        [InlineData("id", Core.Enum.Paginated.SearchOrder.Desc)]
        [InlineData("", Core.Enum.Paginated.SearchOrder.Desc)]

        public async Task SearchCategory_WhereReceivesValidSearchInput_ReturnOrderList(
            string orderBy,
            Core.Enum.Paginated.SearchOrder order,
            string search = "",
            int page = 1,
            int perPage = 10)
        {

            var elastic = serviceProvider.GetRequiredService<IElasticClient>();
            var mediator = serviceProvider.GetRequiredService<IMediator>();

            var examples = GetListCategories(page*perPage);
            var expectedList = GetCloneListCategory(examples, orderBy, order);

            await elastic.IndexManyAsync(examples);
            await elastic.Indices.RefreshAsync(StartIndex.IndexName.Category);

            var inPut = new ListCategoryInput(page, perPage, search, orderBy, order);
            var outPut = await mediator.Send(inPut,CancellationToken.None);

            Assert.NotNull(outPut);
            Assert.NotNull(outPut.Itens);
            Assert.True(outPut.Page == page);
            Assert.True(outPut.PerPage == perPage);
            Assert.True(outPut.Itens.Count() == examples.Count);
            Assert.True(outPut.Total == examples.Count);

            for(int i = 0; i < outPut.Itens.Count; i++)
            {
                var itemExpected = expectedList[i];
                var itemOutPut = outPut.Itens[i];
                Assert.NotNull(itemExpected);   
                Assert.NotNull(itemOutPut);
                Assert.Equal(itemExpected.Name, itemOutPut.Name);
                Assert.Equal(itemExpected.IsActive, itemOutPut.IsActive);   
            }
        }

        public void Dispose() => DeleteAllDocuments();

    }
}