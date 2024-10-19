using Entity = MShop.Catalog.Domain.Entity;
using MShop.Catalog.Infra.Data.ES.Model;
using MShop.Catalog.Infra.Data.ES.Repositoty;
using MShop.Catalog.UnitTests.Infra.Repository.Common;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace MShop.Catalog.UnitTests.Infra.Repository.Category
{
    public class CategoryRespositoryTest : RepositoryTestFixture
    {
        public CategoryRespositoryTest() : base ()
        {
            
        }

        [Fact(DisplayName = nameof(CreateCategory))]
        [Trait("Repository", "Category")]
        public async void CreateCategory()
        {
            var imput = Faker(Guid.NewGuid());
            var model = CategoryModel.EntityToModel(imput);


            var mockResponse = Substitute.For<IndexResponse>();
            mockResponse.IsValid.Returns(true);


            var elastic = Substitute.For<IElasticClient>();

            elastic.IndexDocumentAsync<CategoryModel>(Arg.Any<CategoryModel>(), CancellationToken.None)
                .Returns(Task.FromResult(mockResponse));
            
            var respository = new CategoryRepository(elastic);
            var result = await respository.Create(imput, CancellationToken.None);
            Assert.True(result);         

        }
    }
}
