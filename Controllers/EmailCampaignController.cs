using GroupMailer.Interfaces;
using GroupMailer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroupMailer.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmailCampaignController : ControllerBase
    {
        private readonly IEmailCampaignService _emailCampaignService;

        public EmailCampaignController(IEmailCampaignService emailCampaignService)
        {
            _emailCampaignService = emailCampaignService;
        }

        [HttpPut]
        public async Task<IActionResult> AddEmailCampaign([FromBody] CampaignEmailDto campaignEmailDto)
        {
           var response = await _emailCampaignService.CreateCampaign(campaignEmailDto);
            return Ok(response);
        }
    }
}
