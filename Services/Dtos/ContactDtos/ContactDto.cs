using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dtos.ContactDtos
{
    public class ContactDto
    {

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Phone]
        [Required]
        [StringLength(11)]
        [RegularExpression(@"^01[0-2,5]{1}[0-9]{8}$", ErrorMessage = "In Valid This Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public static ContactDto FromEntity(Contact contact)
        {
            return new ContactDto
            {
                Name = contact.Name,
                PhoneNumber = contact.PhoneNumber,
                Email = contact.Email
            };
        }
    }
}
