using Microsoft.AspNetCore.Mvc;
using PaaS.Services; 

namespace PaaS.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;


        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        
    }
}
