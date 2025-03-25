using GroupMailer.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace GroupMailer.RabbitMq
{
    public class EmailCampaignConsumer
    {
        private readonly EmailSettings _config;


        public EmailCampaignConsumer(IOptions<EmailSettings> config)
        {
            _config = config.Value;
        }
        public async Task StartListening()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();


            await channel.QueueDeclareAsync(queue: "email_campaigns",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);


            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var campaign = JsonConvert.DeserializeObject<EmailCampaign>(message);

                if (campaign != null)
                {
                    await SendEmail(campaign);
                }

                await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
            };


            await channel.BasicConsumeAsync(queue: "email_campaigns",
                                 autoAck: false,
                                 consumer: consumer);

            await Task.Delay(Timeout.Infinite);
        }

        private async Task SendEmail(EmailCampaign campaign)
        {
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(_config.SenderName, _config.SenderEmail));

                foreach(var email in campaign.EmailList)
                {
                    emailMessage.To.Add(new MailboxAddress("", email));
                }

                emailMessage.Subject = campaign.Subject;
                emailMessage.Body = new TextPart("plain") { Text = campaign.Body };

                using var client = new SmtpClient();
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await client.ConnectAsync(_config.SmtpServer, _config.Port, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_config.Username, _config.Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);

                Console.WriteLine($"[✔] Wysłano kampanię: {campaign.Subject}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[!] Błąd wysyłki e-maila: {ex.Message}");
            }
        }
    }
}
