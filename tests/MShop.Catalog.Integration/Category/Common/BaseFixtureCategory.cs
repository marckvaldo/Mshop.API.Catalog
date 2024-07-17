using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MShop.Calalog.Application;
using MShop.Catalog.Infra.Data.ES;
using MShop.Catalog.Infra.Data.ES.Model;
using MShop.Catalog.Infra.Data.ES.StartIndex;
using MShop.Catalog.Integration.Common;
using Nest;
using Application = MShop.Calalog.Application.UseCases.Category.CreateCategory;

namespace MShop.Catalog.Integration.Category.Common
{
    public class BaseFixtureCategory : BaseFixture
    {
        protected IServiceProvider serviceProvider { get; set; }
        public BaseFixtureCategory() : base()
        {
            serviceProvider = BuilderProvider();            
        }

        private IServiceProvider BuilderProvider()
        {
            var service = new ServiceCollection();

            var inMemorySettings = new Dictionary<string, string>()
            {
                {"ConnectionStrings:ElasticSearch","http://localhost:9200" },
                {"ConnectionStrings:Username","elastic" },
                {"ConnectionStrings:Password","password" }                
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            service.AddElasticSearch(configuration)                
                .AddRepository()
                .AddUseCases();

            return service.BuildServiceProvider();

        }



        protected IList<CategoryModel> GetListCategories(int count = 10)
        {
            return Enumerable.Range(0, count)
                    .Select(_ => CategoryModel.EntityToModel(GetCategoryDomainValid()))
                    .ToList();

        }

        protected Application.CreateCategoryInPut GetCategoryValid()
        {
            return new(Guid.NewGuid(), Fake().Name, true);
        }

        protected Domain.Entity.Category GetCategoryDomainValid()
        {
            return new(Fake().Name, Guid.NewGuid(), true);
        }

        protected Application.CreateCategoryInPut GetCategoryValid(Guid Id, string name, bool isActive = true)
        {

            return new(Id, name, true);
        }

        protected CategoryFake Fake()
        {
            return new CategoryFake
            {
                Name = GetNameCategoryValid(),
                IsActive = true,
                IsValid = true
            };
        }


        private string GetNameCategoryValid()
        {
            string category = faker.Commerce.Categories(1)[0];
            while (category.Length < 3)
            {
                category = faker.Commerce.Categories(1)[0];
            }

            if (category.Length > 30)
                category = category[..30];

            return category;
        }

        public static IEnumerable<object[]> ListNamesCategoryInvalid()
        {
            yield return new object[] { InvalidData.GetNameCategoryGreaterThan30CharactersInvalid() };
            yield return new object[] { InvalidData.GetNameCategoryLessThan3CharactersInvalid() };
            yield return new object[] { "" };
            yield return new object[] { null };
        }


        public void DeleteAllDocuments()
        {
            var elastic = serviceProvider.GetRequiredService<IElasticClient>();
            elastic.DeleteByQuery<CategoryModel>(d => d.Query(q => q.QueryString(qs => qs.Query("*")))
            .Conflicts(Elasticsearch.Net.Conflicts.Proceed)
            );
        }

        public async void CreateIndex()
        {
            var index = new StartIndex();
            await index.CreateIndex(serviceProvider);
        }

        public void Deleteindex()
        {
            var index = new StartIndex();
            index.DeleteIndex(serviceProvider);
        }


    }

    public class CategoryFake
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsValid { get; set; }

    }
}

