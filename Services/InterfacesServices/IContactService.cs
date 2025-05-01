using Domain.Models;
using Services.Dtos.ContactDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InterfacesServices
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllContactsAsync();
        Task<ContactDto> CreateContactAsync(ContactDto Entity);
        Task<ContactDtoGetByID> GetContactByIdAsync(int id);
        Task<Contact> UpdateContactAsync(int id, EditContactDto contact);
        Task<bool> RemoveContactAsync(int id);
    }
}
