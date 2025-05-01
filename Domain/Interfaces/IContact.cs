using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IContact : IRepository<Contact>
    {
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<Contact> UpdateAsync(int Id, Contact contact);
        Task<bool> RemoveAsync(int id);
    }
}
