using System.Linq.Expressions;

namespace OnlineShop.Core.Interfaces
{
    public interface IRepository <T> where T : class
    {
        ValueTask<IEnumerable<T>> GetAllAsync(string[] includes = null, bool IgnoreGlobalFilters = false);
        ValueTask<T> GetById(string id);
        ValueTask AddAsync (T entity);
        void UpdateAsync (T entity);
        ValueTask DeleteAsync (string id);

        ValueTask<T> FindAsync(Expression<Func<T, bool>> expression,
            string[] includes = null,
            bool IgnoreGlobalFilters = false);
        ValueTask<IEnumerable<T>> FindAllAsync(
            Expression<Func<T, bool>> expression,
            int? take = null,
            int? skip = null,
            Expression<Func<T, object>> orderBy = null,
            string orderDirection = null,
            string[] includes = null,
            bool IgnoreGlobalFilters = false
         );
    }
}
