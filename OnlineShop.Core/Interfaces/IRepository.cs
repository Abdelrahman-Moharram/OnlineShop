using System.Linq.Expressions;

namespace OnlineShop.Core.Interfaces
{
    public interface IRepository <T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(
            string[] includes = null, 
            bool IgnoreGlobalFilters = false,
            Expression<Func<T, T>> Select = null
            );
        Task<T> GetById(string id);
        Task AddAsync (T entity);
        void UpdateAsync (T entity);
        Task DeleteAsync (string id);

        Task<T> FindAsync(Expression<Func<T, bool>> expression,
            string[] includes = null,
            bool IgnoreGlobalFilters = false);
        Task<IEnumerable<T>> FindAllAsync(
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
