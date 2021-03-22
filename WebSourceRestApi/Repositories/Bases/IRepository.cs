using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebSourceRestApi.Repositories.Bases
{
    public interface IRepository<T>
        where T: class, new()
    {
        Task<T> InsertAsync(T model);
        Task<bool> UpdateAsync(T model);
        Task<bool> AnyAsync(Expression<Func<T, bool>> where);
        Task<bool> DeleteAsync(params object[] keys);
        Task<T> GetAsync(params object[] keys);
        Task<IEnumerable<T>> GetAsync();
        Task<int> SaveAsync();
    }
}
