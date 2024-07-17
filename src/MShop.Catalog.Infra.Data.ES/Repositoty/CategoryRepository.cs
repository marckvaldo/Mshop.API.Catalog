using MShop.Catalog.Domain.Entity;
using MShop.Catalog.Domain.Respositories;
using MShop.Catalog.Infra.Data.ES.Model;
using MShop.Core.Paginated;
using Nest;
using System.Linq.Expressions;

namespace MShop.Catalog.Infra.Data.ES.Repositoty
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IElasticClient _client;

        public CategoryRepository(IElasticClient elasticsearchClient)
        {
            _client = elasticsearchClient;
        }

        public async Task<bool> Create(Category entity, CancellationToken cancellationToken)
        {
            var model = CategoryModel.EntityToModel(entity);
            var result = await _client.IndexDocumentAsync(model, cancellationToken);

            //essas configurações foi definida no serviceRegistration
            //aqui eu estou informar ao indexAsync que o model vai ser salvo no index categories, e o Id do documento vai ser o que esta em Id na model
            //var result = await _client.IndexAsync(model, idx => idx.Index(StartIndex.IndexName.Category).Id(model.Id));

            return result.IsValid;
        }

        public async Task DeleteById(Category entity, CancellationToken cancellationToken)
        {
            await _client.DeleteAsync<CategoryModel>(entity.Id, ct:cancellationToken);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> Filter(Expression<Func<Category, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /*
        "query": {
            "match": {
                "name":{
                    query:"home"
            }
          }
        },
        "from": 0,
        "size": 10,
        "sort": [
            {"name.keyword": "asc" }
            {"id": "asc" }
        ]
         * */

        public async Task<PaginatedOutPut<Category>> FilterPaginate(PaginatedInPut input, CancellationToken cancellationToken)
        {
            var result = await _client
                .SearchAsync<CategoryModel>(s => s
                    .Query(q => q
                        .Match(m => m
                            .Field(f => f.Name)
                            .Query(input.Search)
                            )
                        )
                        .From(input.From)
                        .Size(input.PerPage)
                        .Sort(BuildSortExpression(input.OrderBy, input.Order)),
                    ct: cancellationToken);

            var categoriesEntity = new List<Category>();

            result.Documents.ToList().ForEach(c =>
            {
                categoriesEntity.Add(CategoryModel.ModelToEntity(c));
            });

            return new PaginatedOutPut<Category>(
                input.Page,
                input.PerPage,
                (int)result.Total,
                categoriesEntity);

        }

        private static Func<SortDescriptor<CategoryModel>, IPromise<IList<ISort>>> BuildSortExpression(string orderBy, Core.Enum.Paginated.SearchOrder order)
        {
            switch(orderBy.ToLower(), order) {

                case ("name",Core.Enum.Paginated.SearchOrder.Asc):
                    return sort => sort
                           .Ascending(f => f.Name.Suffix("Keyword"))
                           .Ascending(f => f.Id);

                case ("name", Core.Enum.Paginated.SearchOrder.Desc):
                    return sort => sort
                            .Descending(f => f.Name.Suffix("Keyword"))
                            .Descending(f => f.Id);

                case ("id", Core.Enum.Paginated.SearchOrder.Asc):
                    return sort => sort.Ascending(f => f.Id);

                case ("id", Core.Enum.Paginated.SearchOrder.Desc):
                    return sort => sort.Descending(f => f.Id);

                default:
                    return sort => sort
                        .Ascending(f => f.Name.Suffix("Keyword"))
                        .Ascending(f => f.Id);
            }

        }

        public async Task<Category?> GetById(Guid Id)
        {
            var result = await _client.GetAsync<CategoryModel>(Id);
            
            if (result.Source is null)
                return null;

            return CategoryModel.ModelToEntity(result.Source);
        }

        public Task<Category> GetLastRegister(Expression<Func<Category, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> GetValuesList()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task Update(Category entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
