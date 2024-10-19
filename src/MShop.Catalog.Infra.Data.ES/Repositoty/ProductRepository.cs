using Elasticsearch.Net;
using MShop.Catalog.Domain.Entity;
using MShop.Catalog.Domain.Respositories;
using MShop.Catalog.Infra.Data.ES.Model;
using MShop.Core.Paginated;
using Nest;
using System.Linq.Expressions;

namespace MShop.Catalog.Infra.Data.ES.Repositoty
{
    public class ProductRepository : IProductRepository
    {
        private readonly IElasticClient _client;

        public ProductRepository(IElasticClient elasticsearchClient)
        {
            _client = elasticsearchClient;
        }

        public async Task<bool> Create(Product entity, CancellationToken cancellationToken)
        {
            var produtos = ProductModel.EntityToModel(entity);
            var result = await _client.IndexDocumentAsync(produtos, cancellationToken);
            return result.IsValid;
        }

        public async Task DeleteById(Product entity, CancellationToken cancellationToken)
        {
            await _client.DeleteAsync<ProductModel>(entity.Id, ct:cancellationToken);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public Task<List<Product>> Filter(Expression<Func<Product, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginatedOutPut<Product>> FilterPaginate(PaginatedInPut input, CancellationToken cancellationToken)
        {
           var result = await _client.SearchAsync<ProductModel>(s=>s
                .Query(q=>q
                    .Match(m=>m
                        .Field(f=>f.Name)
                        .Query(input.Search)
                        )
                    )
                .From(input.From)
                .Size(input.PerPage)
                .Sort(BuildSortExpression(input.OrderBy,input.Order)),
                ct: cancellationToken);

            var productsEntity = new List<Product>();

            foreach (var item in result.Documents)
            {
                productsEntity.Add(ProductModel.ModelToEntity(item));
            }

            return new PaginatedOutPut<Product>(
                input.Page, input.PerPage, (int) result.Total, productsEntity);
        }

        public static Func<SortDescriptor<ProductModel>, IPromise<IList<ISort>>> BuildSortExpression(string orderBy, MShop.Core.Enum.Paginated.SearchOrder order)
        {
            switch(orderBy.ToLower(), order)
            {
                case ("name", MShop.Core.Enum.Paginated.SearchOrder.Desc):
                    return sort => sort
                    .Descending(f => f.Name)
                    .Descending(f => f.Id);

                case ("name", MShop.Core.Enum.Paginated.SearchOrder.Asc):
                    return sort => sort
                    .Ascending(f => f.Name)
                    .Ascending(f => f.Id);

                default:
                    return sort => sort
                    .Ascending(f => f.Name)
                    .Ascending(f => f.Id);
            }
        }

        public async Task<Product?> GetById(Guid Id)
        {
            var result = await _client.GetAsync<ProductModel>(Id);
            if (result.Source == null)
                return null;

            return ProductModel.ModelToEntity(result.Source);
        }

        public Task<Product> GetLastRegister(Expression<Func<Product, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetProductsByCategoryId(Guid categoryId)
        {
            var result = await _client.SearchAsync<ProductModel>(s => s
                            .Query(q => q
                                .Match(m => m
                                    .Field(f=>f.CategoryId)
                                    .Query(categoryId.ToString())
                                    )
                                )

                            );

            return ProductModel.ListModelToListEntity(result.Documents);
        }

        public Task<List<Product>> GetValuesList()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task Update(Product entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }

}
