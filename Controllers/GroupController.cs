using GroupMailer.DTO;
using GroupMailer.Interfaces;
using GroupMailer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GroupMailer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet("get-contacts")]
        public async Task<IActionResult> GetContactsByGroupId(int groupId)
        {
            var response = await _groupService.GetContactsByGroupId(groupId);
            return Ok(response);
        }

        [HttpGet("get-group-id")]
        public async Task<IActionResult> GetGroupById(int groupId)
        {
            var response = await _groupService.GetGroupById(groupId);
            return Ok(response);
        }

        [HttpGet("get-group-name")]
        public async Task<IActionResult> GetGroupsByName(string name)
        {
            var response = await _groupService.GetGroupsByName(name);
            return Ok(response);
        }

        [HttpPut("add-contact-group")]
        public async Task<IActionResult> AddContactToGroup([FromBody] ContactGroupRequest contactGroupRequest)
        {
            await _groupService.AddContactToGroup(contactGroupRequest.ContactId, contactGroupRequest.GroupId);
            return Ok();
        }

        [HttpPut("add-group")]
        public async Task<IActionResult> AddGroup([FromBody] GroupDto groupDto)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized("Email not found in token.");
            }

            await _groupService.AddGroup(groupDto, userEmail);
            return Ok();
        }

        [HttpDelete("delete-contact-group")]
        public async Task<IActionResult> DeleteContactFromGroup([FromBody] ContactGroupRequest contactGroupRequest)
        {
            await _groupService.DeleteContactFromGroup(contactGroupRequest.ContactId, contactGroupRequest.GroupId);
            return Ok();
        }

        [HttpDelete("delete-group")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var group = await _groupService.GetGroupById(id);
            await _groupService.DeleteGroup(group);
            return Ok();
        }
    }
}
