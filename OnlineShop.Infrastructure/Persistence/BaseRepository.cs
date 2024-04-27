using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using OnlineShop.Core.Constants;
using OnlineShop.Core.Entities;
using OnlineShop.Core.Interfaces;
using OnlineShop.Infrastructure.Data;
using System.Linq.Expressions;

namespace OnlineShop.Core.Persistence
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private ApplicationDbContext _context;
        private DbSet<T> _entity;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _entity = _context.Set<T>(); // should be any type of dbtable class
        }

        private IQueryable<T> HandleIncludes(
            IQueryable<T> query,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool IgnoreGlobalFilters = false, 
            Expression<Func<T, T>> select = null
         )
        {
            if (IgnoreGlobalFilters)
                query = query.IgnoreQueryFilters();

            if (include != null)
                query = include(query);
            
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

        public async Task<List<T>>  GetAllAsync(
            /*Expression<Func<T, bool>> predicate = null,*/
            Expression<Func<T, T>> selector = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = false
            )
        {
            IQueryable<T> query = _entity;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (selector != null)
                query = query.Select(selector);

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        /*public async Task<IEnumerable<T>> GetAllAsync(
                int? take = null,
                int? skip = null,
                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                bool IgnoreGlobalFilters = false

            )
        {
            var query = HandleIncludes(_entity, include, IgnoreGlobalFilters);


            if (take.HasValue)
                query = query.Take(take.Value);
            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }*/



        public async Task DeleteAsync(string id)
        {
            var entity = await _entity.FindAsync(id);
            if(entity != null)
            {
                entity.IsDeleted = true;
                await Task.Run(() => _entity.Update(entity));
            }
        }
        public async Task UpdateAsync(T entity)
        {
            await Task.Run(() => _entity.Update(entity));
        }

        public async Task<T> FindAsync(
            Expression<Func<T, bool>> expression, 
            string[] includes = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            string thenIncludes =null,
            bool IgnoreGlobalFilters=false
        )
        {
            return await HandleIncludes(_entity, include, IgnoreGlobalFilters).SingleOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<T>> FindAllAsync(
            Expression<Func<T, bool>> expression,
            int? take = null,
            int? skip = null,
                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool IgnoreGlobalFilters = false
            )
        {
            var query = HandleIncludes(_entity, include, IgnoreGlobalFilters).Where(expression);


            if (take.HasValue)
                query = query.Take(take.Value);
            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }

        
    }
}
