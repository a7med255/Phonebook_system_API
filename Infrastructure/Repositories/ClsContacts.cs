using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ClsContacts: Repository<Contact> ,IContact
    {
        private readonly PhoneContext context;
        public ClsContacts(PhoneContext context):base(context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            try
            {
                var contacts = await context.TbContacts.Where(a => a.CurrentState == true).ToListAsync();
                return contacts;
            }
            catch
            {

                return new List<Contact>();
            }
        }
        public async Task<Contact> UpdateAsync(int Id, Contact Contact)
        {
            try
            {
                if (Id != 0)
                {
                    context.Update(Contact).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await context.SaveChangesAsync();
                }
                return Contact;
            }
            catch
            {
                return new Contact();
            }
        }
        public async Task<bool> RemoveAsync(int id)
        {
            try
            {
                var Contact = await GetSingleAsync(a => a.Id == id);

                if (Contact == null)
                    return false;

                Contact.CurrentState = false;
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex) { return false; }
        }
    }
}
