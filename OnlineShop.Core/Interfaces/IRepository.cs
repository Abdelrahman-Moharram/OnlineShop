using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace OnlineShop.Core.Interfaces
{
    public interface IRepository <T> where T : class
    {
        /*Task<IEnumerable<T>> GetAllAsync(
            int? take = null,
            int? skip = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string orderDirection = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool IgnoreGlobalFilters = false
            );*/
        Task<List<T>> GetAllAsync(
            /*Expression<Func<T, bool>> predicate = null,*/
            Expression<Func<T, T>> selector = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true
            );
        Task<T> GetById(string id);
        Task AddAsync (T entity);
        Task UpdateAsync (T entity);
        Task DeleteAsync (string id);

        Task<T> FindAsync(
            Expression<Func<T, bool>> expression,
            string[] includes = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            string thenIncludes = null,
            bool IgnoreGlobalFilters = false
            );
        Task<IEnumerable<T>> FindAllAsync(
            Expression<Func<T, bool>> expression,
            int? take = null,
            int? skip = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool IgnoreGlobalFilters = false
         );
    }
}
