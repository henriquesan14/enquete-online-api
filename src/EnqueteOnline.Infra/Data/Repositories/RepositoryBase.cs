using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EnqueteOnline.Infra.Data.Repositories
{
    public class RepositoryBase<TEntity, TId> : IAsyncRepository<TEntity, TId> where TEntity : Entity<TId>, IAggregate<TId>
    {
        protected readonly EnqueteOnlineDbContext DbContext;

        public RepositoryBase(EnqueteOnlineDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();
            if (disableTracking) query = query.AsNoTracking();
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, List<Expression<Func<TEntity, object>>> includes = null,
          bool disableTracking = true, int? pageNumber = null, int? pageSize = 20)
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (pageNumber != null)
            {
                query = query.Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }


        public async Task<TEntity> GetByIdAsync(TId id, bool disableTracking = false, List<Expression<Func<TEntity, object>>> includes = null)
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool disableTracking = false, List<Expression<Func<TEntity, object>>> includes = null)
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await DbContext.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public void Update(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
        }

        public virtual async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null
            ? await DbContext.Set<TEntity>().CountAsync()
            : await DbContext.Set<TEntity>().CountAsync(predicate);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await DbContext.Set<TEntity>().AddRangeAsync(entities);
        }
    }
}
