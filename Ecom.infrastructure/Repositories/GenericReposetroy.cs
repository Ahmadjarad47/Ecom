using Ecom.Core.Interfaces;
using Ecom.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositories
{
    public class GenericReposetroy<T> : IGenericRepositrie<T> where T : class
    {
        private readonly ApplicationDbContext context;

        public GenericReposetroy(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T id)
        {
          var entity=  await context.Set<T>().FindAsync(id);
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll()
        => context.Set<T>().AsNoTracking().ToList();

      

        public async Task<IReadOnlyList<T>> GetAllAsync()
        =>await context.Set<T>().AsNoTracking().ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            var query=context.Set<T>().AsQueryable();
            foreach (var item in includes)
            {
                query=query.Include(item);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(T id)
       =>await context.Set<T>().FindAsync(id);

        public async Task<T> GetByIdAsync(T id, params Expression<Func<T, object>>[] includes)
        {
           IQueryable<T> values = context.Set<T>();
            foreach (var item in includes)
            {
                values = values.Include(item);
            }
            return await ((DbSet<T>)values).FindAsync(id);
        }

        public async Task UpdateAsync(T id,T entity)
        {
            var entities = await context.Set<T>().FindAsync(id);
         context.Update(entities);
            await context.SaveChangesAsync();
        }
    }
}
