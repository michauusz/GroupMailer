using GroupMailer.Interfaces;
using GroupMailer.Models;
using GroupMailer.RabbitMq;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;

namespace GroupMailer.Services
{
    public class EmailCampaignService : IEmailCampaignService
    {
        private readonly EmailCampaignProducer _producer;
        private readonly IGroupQueryRepository _groupQueryRepository;

        public EmailCampaignService(EmailCampaignProducer producer, IGroupQueryRepository groupQueryRepository)
        {
            _producer = producer;
            _groupQueryRepository = groupQueryRepository;
        }

        public async Task<string> CreateCampaign(CampaignEmailDto campaignEmailDto)
        {
            var emailList = await _groupQueryRepository.GetContactsEmailsByGroupIdAsync(campaignEmailDto.TargetGroupId);
            var campaign = new EmailCampaign
            {
                Subject = campaignEmailDto.Subject,
                Body = campaignEmailDto.Body,
                UserId = campaignEmailDto.UserId,
                EmailList = emailList
            };

            //rejestracja w bazie

            await _producer.SendEmailCampaign(campaign);


            return "Ok(campaign);";
        }
    }
}
