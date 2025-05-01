using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly PhoneContext context;
        private readonly DbSet<T> dbSet;
        public Repository(PhoneContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> filter)
        {
            try
            {
                IQueryable<T> query = dbSet;

                var result = await query.FirstOrDefaultAsync(filter);

                return result;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public async Task<T> AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");

            try
            {
                await dbSet.AddAsync(entity);
                await context.SaveChangesAsync();

                return entity; 
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the entity.", ex);
            }
        }

    }
}
