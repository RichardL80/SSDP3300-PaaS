using PaaS.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaaS.ViewModels;
using PaaS.Models;
using PaaS.Services;
using PaaS.Repositories;

namespace PaaS.Controllers
{
    public class PayPalController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CartService _cartService;
        private readonly ContactRepo _contactRepo;
        private readonly UserRepo _userRepo;
        private readonly LocationRepo _locationRepo;

        public PayPalController(ApplicationDbContext context, CartService cartService, ContactRepo contactRepo, UserRepo userRepo, LocationRepo locationRepo)
        {
            _context = context;
            _cartService = cartService;
            _contactRepo = contactRepo;
            _userRepo = userRepo;
            _locationRepo = locationRepo;
        }

        [Authorize]
        public IActionResult Index()
        {
            // Get the user's contact info
            string userEmail = User.Identity?.Name;
            User user = _userRepo.GetUser(userEmail);
            IEnumerable<ContactInfoVM> contactInfoVM = _contactRepo.GetContactInfo(user.UserId); // Get contact info for the user
            foreach (var contact in contactInfoVM)
            {
                // Get the city and province names for each contact
                contact.CityName = _locationRepo.GetCityName(contact.CityId);
                contact.ProvinceName = _locationRepo.GetProvinceName(contact.ProvinceId);
            }
            // Get the cart items
            CheckoutVM checkoutVM = new CheckoutVM
            {
                CartItems = _cartService.GetCart(),
                ContactInfo = contactInfoVM
            };

            return View(checkoutVM);
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

        [HttpGet]
        public JsonResult GetCart()
        {
            var cart = _cartService.GetCart();
            return Json(cart);
        }
    }
}