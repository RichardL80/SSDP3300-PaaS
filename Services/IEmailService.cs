using PaaS.Models;
using SendGrid;

namespace PaaS.Services
{
    public interface IEmailService
    {
        Task<Response> SendSingleEmail(ComposeEmailModel payload);
    }
}


