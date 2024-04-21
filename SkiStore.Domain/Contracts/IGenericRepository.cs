
using System.Linq.Expressions;

namespace SkiStore.Domain.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<TResult> GetAsync<TResult>(Expression<Func<T,bool>> filter, params string[] properties);

        Task<List<TResult>> GetAllAsync<TResult>(int pageNmuber,
            int pageSize, Expression<Func<T, bool>?> filter=null, Expression<Func<T, object>?> order = null, Expression<Func<T, object>?> orderDesc = null, params string[] properties);

        //Task<QueryResult<TResult>> GetAllAsync<TResult>(QueryPerimeters query);

        Task<int> GetCountAsync(Expression<Func<T, bool>?> filter);

        Task<TResult> AddAsync<TSource,TResult>(TSource entity);

        Task<int> UpdateAsync<TSource>( int id,TSource source);

        Task<int> DeleteAsync(int id);

        Task<bool> Exists(Expression<Func<T,bool>>filter);
    }
}
