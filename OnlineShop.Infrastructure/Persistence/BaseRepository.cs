using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using OnlineShop.Core.Constants;
using OnlineShop.Core.Interfaces;
using OnlineShop.Infrastructure.Data;
using System.Linq.Expressions;

namespace OnlineShop.Core.Persistence
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private ApplicationDbContext _context;
        private DbSet<T> _entity;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _entity = _context.Set<T>(); // should be any type of dbtable class
        }

        private IQueryable<T> HandleIncludes(IQueryable<T> query, string[] includes = null, bool IgnoreGlobalFilters = false, Expression<Func<T, T>> select = null)
        {
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include).AsNoTracking();
            if (IgnoreGlobalFilters)
                query = query.IgnoreQueryFilters();
            if(select != null)
                return query.Select(select);
            return query;
        }


        public async Task AddAsync(T entity)
        {
            await _entity.AddAsync(entity);
        }

        public async Task<T> GetById(string id)
        {
            return await _entity.FindAsync(id);
        }


        public async Task<IEnumerable<T>> GetAllAsync(
                int? take = null,
                int? skip = null,
                Expression<Func<T, object>> orderBy = null,
                string orderDirection = null,
                string[] includes = null,
                bool IgnoreGlobalFilters = false

            )
        {
            var query = HandleIncludes(_entity, includes, IgnoreGlobalFilters);


            if (take.HasValue)
                query = query.Take(take.Value);
            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if (orderBy != null)
            {
                if (orderDirection == OrderDirections.Descending)
                    query.OrderByDescending(orderBy);
                else
                    query.OrderBy(orderBy);
            }

            return await query.ToListAsync();
        }



        public async Task DeleteAsync(string id)
        {
            var entity = await _entity.FindAsync(id);
            if(entity != null)
                await Task.Run(() => _entity.Remove(entity));
        }
        public void UpdateAsync(T entity)
        {
            _entity.Remove(entity);
        }

        public async Task<T> FindAsync(
            Expression<Func<T, bool>> expression, 
            string[] includes = null, 
            bool IgnoreGlobalFilters = false
        )
        {
            return await HandleIncludes(_entity, includes, IgnoreGlobalFilters).SingleOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<T>> FindAllAsync(
            Expression<Func<T, bool>> expression,
            int? take = null,
            int? skip = null,
            Expression<Func<T, object>> orderBy = null,
            string orderDirection = null,
            string[] includes = null,
            bool IgnoreGlobalFilters = false
            )
        {
            var query = HandleIncludes(_entity, includes, IgnoreGlobalFilters).Where(expression);


            if (take.HasValue)
                query = query.Take(take.Value);
            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if (orderBy != null)
            {
                if (orderDirection == OrderDirections.Descending)
                    query.OrderByDescending(orderBy);
                else
                    query.OrderBy(orderBy);
            }

            return await query.ToListAsync();
        }


    }
}
