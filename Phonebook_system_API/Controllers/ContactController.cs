using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos.ContactDtos;
using Services.InterfacesServices;
using System.Net;

namespace Phonebook_system_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly API_Resonse response; 
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
            response = new API_Resonse();
        }
        [HttpGet("GetAllContacts")]
        public async Task<ActionResult<API_Resonse>> GetAllContacts()
        {
            var contacts = await _contactService.GetAllContactsAsync();
            response.Result =contacts;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Ok(response);
        }
        [HttpPost("CreateContactAsync")]
        public async Task<ActionResult<API_Resonse>> CreateContactAsync([FromBody] ContactDto CreateContactDto)
        {
            if (!ModelState.IsValid)
            {
                response.Result = string.Empty;
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(response);
            }

            var contact= await _contactService.CreateContactAsync(CreateContactDto);

            if(contact!=null)
            {
                response.Result = contact;
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.Created;
                return Ok(response);
            }


            response.Result = new { message = "Erorr In Add" };
            response.IsSuccess = false;
            response.StatusCode = HttpStatusCode.BadRequest;
            return BadRequest(response);
        }
        [HttpGet("GetContactById/{Id}")]
        public async Task<ActionResult<API_Resonse>> GetContactById(int Id)
        {
            var Contact = await _contactService.GetContactByIdAsync(Id);

            if (Contact is null)
            {
                response.Result = string.Empty;
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.Errors.Add("Contact does not exist.");
                return NotFound(response);
            }
            response.Result = Contact;
            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            return Ok(response);
        }
        [HttpPost("EditContactAsync")]
        public async Task<ActionResult<API_Resonse>> EditContactAsync(int Id, EditContactDto contacts)
        {

            if (!ModelState.IsValid)
            {
                response.Result = string.Empty;
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(response);
            }
            
            

            var Contact = await _contactService.UpdateContactAsync(Id, contacts);


            if (Contact.Id ==0)
            {
                response.Result = string.Empty;
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.Errors.Add("Contact does not exist.");
                return NotFound(response);
            }
            response.Result = Contact;
            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            return Ok(response);
        }
        [HttpDelete("RemoveContact/{Id}")]
        public async Task<IActionResult> RemoveContact(int Id)
        {

              var contact=  await _contactService.RemoveContactAsync(Id);

            if (contact)
            {
                response.Result = new { message = "Removed Sucssfully" };
                response.StatusCode = HttpStatusCode.OK;
                response.IsSuccess = true;
                return Ok(response);
            }
                response.Result = string.Empty;
                response.StatusCode = HttpStatusCode.NotFound;  
                response.IsSuccess = false;
                response.Errors.Add("Erorr");
                return NotFound(response);
        }
    }
}
