using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetSingleAsync(Expression<Func<T, bool>> filter);
        Task<T> AddAsync(T Entity);
    }
}
