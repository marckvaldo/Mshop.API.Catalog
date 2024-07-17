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

        public Task<bool> Create(Product entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task DeleteById(Product entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> Filter(Expression<Func<Product, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedOutPut<Product>> FilterPaginate(PaginatedInPut input, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Product?> GetById(Guid Id)
        {
            throw new NotImplementedException();
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
