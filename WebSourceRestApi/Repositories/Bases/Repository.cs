using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebSourceRestApi.Services;

namespace WebSourceRestApi.Repositories.Bases
{
    public abstract class Repository<T>: IRepository<T>
        where T : class, new()
    {
        public DatabaseService Service { get; }
        public DbSet<T> Model { get; }
        public Repository(DatabaseService service)
        {
            Service = service;
            Model = service.Set<T>();
        }

        public async Task<T> InsertAsync(T model)
        {
            await Model.AddAsync(model);
            await SaveAsync();
            return model;
        }

        public async Task<bool> UpdateAsync(T model)
        {
            Model.Update(model);
            return await SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(params object[] keys)
        {
            var model = await GetAsync(keys);
            if (model != null)
            {
                Service.Remove(model);
                return await SaveAsync() > 0;
            }
            return false;
        }

        public async Task<T> GetAsync(params object[] keys)
        {
            return await Model.FindAsync(keys);
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await Model.ToListAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await Service.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> where)
        {
            return await Model.AnyAsync(where);
        }
    }
}
 