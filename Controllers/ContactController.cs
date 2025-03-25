using Azure.Core;
using GroupMailer.DTO;
using GroupMailer.Interfaces;
using GroupMailer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GroupMailer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int userId, int contactId)
        {
            await _contactService.DeleteContactAsync(userId, contactId);
            return Ok();
        }

        [HttpPut("add-existing")]
        public async Task<IActionResult> AddExistingContact([FromBody] UserContactRequest request)
        {
            var result = await _contactService.AddExistingContactAsync(request.UserId, request.ContactId);
            return Created();
        }

        [HttpPut("add-new")]
        public async Task<IActionResult> AddNewContact([FromBody] ContactCreateDto request)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized("Email not found in token.");
            }

            var newContact = new Contact
            {
                Name = request.Name,
                Email = request.Email,
                CustomName = request.CustomName
            };

            var result = await _contactService.AddNewContactAsync(newContact, userEmail);
            return Created();
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _contactService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("get-all-other")]
        public async Task<IActionResult> GetAllOtherCOntactAsync(int userId)
        {
            var result = await _contactService.GetAllOtherCOntactAsync(userId);
            return Ok(result);
        }

        [HttpGet("get-all-user")]
        public async Task<IActionResult> GetAllUserContactAsync(int userId)
        {
            var result = await _contactService.GetAllUserContactAsync(userId);
            return Ok(result);
        }

        [HttpGet("get-byId")]
        public async Task<IActionResult> GetContactByIdAsync(int userId)
        {
            var result = await _contactService.GetContactByIdAsync(userId);
            return Ok(result);
        }

        [HttpPatch]
        public async Task<IActionResult> Update(ContactUpdateDto request)
        {
            await _contactService.ModifyConctactAsync(request);
            return Ok();
        }

    }
}
