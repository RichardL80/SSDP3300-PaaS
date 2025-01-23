using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;
using PaaS.Models;

namespace PaaS.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Response> SendSingleEmail(ComposeEmailModel payload)
        {
            var apiKey = _configuration["SendGrid:ApiKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("griguta.vladmihai1@gmail.com", "Vlad Griguta");
            var to = new EmailAddress(payload.Email);
            var msg = MailHelper.CreateSingleEmail(from, to, payload.Subject, payload.Body, payload.Body);
            return await client.SendEmailAsync(msg);
        }
    }
}
