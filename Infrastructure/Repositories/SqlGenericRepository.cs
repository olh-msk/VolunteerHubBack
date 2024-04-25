using Application.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class SqlGenericRepository<TEntity, IdType> : IGenericRepository<TEntity, IdType> where TEntity : class
    {
        protected readonly ApplicationContext _applicationContext;
        protected readonly DbSet<TEntity> _entity;
        public SqlGenericRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            _entity = _applicationContext.Set<TEntity>();
        }
        public virtual async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string[]? includeProperties = null)
        {
            IQueryable<TEntity> query = _entity.AsNoTracking();

            if (includeProperties != null)
            {
                foreach (string? includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }
        public virtual async Task<TEntity?> GetByIdAsync(IdType id)
        {
            return await _entity.FindAsync(id);
        }
        public virtual async Task<bool> InsertAsync(TEntity entity)
        {
            await _entity.AddAsync(entity);
            return true;
        }
        public virtual async Task<bool> DeleteAsync(IdType id)
        {
            TEntity? entityToDelete = await _entity.FindAsync(id);

            if (entityToDelete != null)
            {
                _entity.Remove(entityToDelete);
                return true;
            }
            return false;
        }
        public virtual Task<bool> UpdateAsync(TEntity entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}