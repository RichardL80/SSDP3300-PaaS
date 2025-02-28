using PaaS.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaaS.ViewModels;
using PaaS.Models;

namespace PaaS.Controllers
{
    public class PayPalController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PayPalController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("PayPal/PayPalConfirmation")]
        public IActionResult PayPalConfirmation(string transactionId, string amount, string payerName, string email)
        {
            var parsedAmount = decimal.TryParse(amount, out decimal newAmount);
            var payPalConfirmation = new PayPalConfirmationModel
            {
                TransactionId = transactionId,
                Amount = newAmount,
                PayerName = payerName,
                Email = email,
                CreatedDate = DateTime.UtcNow
            };
            _context.PayPalConfirmationModel.Add(payPalConfirmation);
            _context.SaveChanges();

            var transactionVM = new PayPalTransactionVM
            {
                TransactionId = transactionId,
                Amount = newAmount,
                PayerName = payerName,
                Email = email
            };

            return View(transactionVM);
        }

        public IActionResult Transaction()
        {
            var paypalDetail = _context.PayPalConfirmationModel.Select(p => new PayPalTransactionVM
            {
                TransactionId = p.TransactionId,
                Amount = p.Amount,
                PayerName = p.PayerName,
                CreatedDate = p.CreatedDate,
                Email = p.Email,
                MOP = "MOP"
            }).ToList();
            return View(paypalDetail);
        }
    }
}