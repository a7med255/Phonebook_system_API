using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class PhoneContext : DbContext
    {
        public PhoneContext()
        {
            
        }
        public PhoneContext(DbContextOptions<PhoneContext> options) 
            : base(options)
        {
        }

        public virtual DbSet<Contact> TbContacts { get; set; }
    }
}
