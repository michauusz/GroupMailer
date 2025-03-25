using GroupMailer.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace GroupMailer.RabbitMq
{
    public class EmailCampaignProducer
    {
        public async Task SendEmailCampaign(EmailCampaign campaign)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            var message = JsonConvert.SerializeObject(campaign);
            var body = Encoding.UTF8.GetBytes(message);

            await channel.QueueDeclareAsync(queue: "email_campaigns",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);


            await channel.BasicPublishAsync(exchange: "",
                                 routingKey: "email_campaigns",
                                 body: body);
        }
    }
}
