using GroupMailer.Models;

namespace GroupMailer.Interfaces
{
    public interface IEmailCampaignService
    {
        Task<string> CreateCampaign(CampaignEmailDto campaignEmailDto);
    }
}
