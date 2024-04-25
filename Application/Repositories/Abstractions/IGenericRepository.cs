using System.Linq.Expressions;

namespace Application.Repositories.Abstractions
{
    public interface IGenericRepository<TEntity, IdType> where TEntity : class
    {
        public Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string[]? includeProperties = null);
        public Task<TEntity?> GetByIdAsync(IdType id);
        public Task<bool> InsertAsync(TEntity entity);
        public Task<bool> UpdateAsync(TEntity entityToUpdate);
        public Task<bool> DeleteAsync(IdType id);
    }
}