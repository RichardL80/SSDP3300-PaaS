using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;
using PaaS.Models;

namespace PaaS.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _apiKey;

        public EmailService(IConfiguration configuration)
        {
            _apiKey = configuration["SendGrid:ApiKey"];
        }

        public async Task<Response> SendSingleEmail(ComposeEmailModel payload)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress("griguta.vladmihai1@gmail.com", "Vlad Griguta");
            var to = new EmailAddress(payload.Email);

            var msg = MailHelper.CreateSingleEmail(from, to, payload.Subject, payload.Body, payload.Body);
            return await client.SendEmailAsync(msg);
        }
    }
}

