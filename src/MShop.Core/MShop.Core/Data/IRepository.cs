using MShop.Core.DomainObject;
using MShop.Core.Paginated;
using System.Linq.Expressions;

namespace MShop.Core.Data
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<bool> Create(TEntity entity, CancellationToken cancellationToken);
        Task Update(TEntity entity, CancellationToken cancellationToken);
        Task DeleteById(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity?> GetById(Guid Id);
        Task<List<TEntity>> GetValuesList();
        Task<List<TEntity>> Filter(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetLastRegister(Expression<Func<TEntity, bool>> predicate);
        Task<PaginatedOutPut<TEntity>> FilterPaginate(PaginatedInPut input, CancellationToken cancellationToken);
        Task<int> SaveChanges();
    }
}
