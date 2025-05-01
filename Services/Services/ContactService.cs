using Domain.Interfaces;
using Domain.Models;
using Services.Dtos.ContactDtos;
using Services.InterfacesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ContactService: IContactService
    {
        private readonly IContact _contactRepository;
        public ContactService(IContact contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            return await _contactRepository.GetAllAsync();
        }

        public async Task<ContactDto> CreateContactAsync(ContactDto ContactDto)
        {

            var contact= new Contact()
                        {
                            Name = ContactDto.Name,
                            PhoneNumber = ContactDto.PhoneNumber,
                            Email = ContactDto.Email,
                            CreatedDate = DateTime.UtcNow,
                            CreatedBy = "Admin",
                            CurrentState = true
                        };

             var addedContact= await _contactRepository.AddAsync(contact);

            return ContactDto.FromEntity(contact);

        }

        public async Task<ContactDtoGetByID> GetContactByIdAsync(int id)
        {
            var contact = await _contactRepository.GetSingleAsync(a => a.Id == id);
            if (contact == null)
            {
                return null;
            }

            return ContactDtoGetByID.FromEntity(contact);
        }

        public async Task<Contact> UpdateContactAsync(int id, EditContactDto contact)
        {
            var UpdatedContact = await _contactRepository.GetSingleAsync(x => x.Id == id);
            if (UpdatedContact is not null)
            {
                UpdatedContact.Email = contact.Email;
                UpdatedContact.PhoneNumber = contact.PhoneNumber;
                UpdatedContact.Name = contact.Name;
                UpdatedContact.UpdatedDate = DateTime.UtcNow;
                UpdatedContact.UpdatedBy = "Admin";
                var UpdateContact = await _contactRepository.UpdateAsync(id, UpdatedContact);
                return UpdateContact;
            }
            return new Contact();
        }

        public async Task<bool> RemoveContactAsync(int id)
        {
            if (id == 0)
            {
                return false;
            }
            return await _contactRepository.RemoveAsync(id);
        }
    }
}
