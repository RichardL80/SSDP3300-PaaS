using Microsoft.AspNetCore.Mvc;
using PaaS.Models;
using PaaS.Repositories;
using PaaS.ViewModels;
using Microsoft.EntityFrameworkCore;
using PaaS.Data;
using Microsoft.AspNetCore.Authorization;
using PaaS.Services;
using PaaS.Repositories;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace PaaS.Controllers
{
    public class OrderSummaryController : Controller
    {
        private readonly OrderRepo _orderRepo;
        private readonly ApplicationDbContext _context;

        private readonly CartService _cartService;

        private readonly UserRepo _userRepo;

        private readonly ContactRepo _contactRepo;
        private readonly LocationRepo _locationRepo;

        public OrderSummaryController(ApplicationDbContext context, CartService cartService, ContactRepo contactRepo, UserRepo userRepo, LocationRepo locationRepo, OrderRepo orderRepo)
        {
            _context = context;
            _cartService = cartService;
            _contactRepo = contactRepo;
            _userRepo = userRepo;
            _locationRepo = locationRepo;
            _orderRepo = orderRepo;
        }
        public IActionResult Index(int orderId = 1) // Default to orderId=1 if not provided
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

            // Get the cart items, and round the price to 2 decimal
            var cartItems = _cartService.GetCart();
            var subtotal = Math.Round(cartItems.Sum(x => x.Quantity * x.Item.Price), 2);
            var gst = Math.Round(subtotal * 0.05m, 2);
            var pst = Math.Round(subtotal * 0.07m, 2);
            var shippingFee = Math.Round(subtotal * 0.02m, 2);
            var total = Math.Round(subtotal + gst + pst + shippingFee, 2);

            OrderSummaryVM orderSummaryVM = new OrderSummaryVM
            {
                CartItems = cartItems,
                ContactInfo = contactInfoVM,
                OrderDate = DateTime.Now,
                Subtotal = subtotal,
                GST = gst,
                PST = pst,
                ShippingFee = shippingFee,
                Total = total
            };

            return View(orderSummaryVM);
        }

        // Save order after place it 
        [HttpPost]
        public IActionResult SaveOrder(int deliveryMethodId, int paymentMethodId, string selectedAddress)
        {
            string userEmail = User.Identity?.Name;
            User user = _userRepo.GetUser(userEmail);

            var cartItems = _cartService.GetCart();
            if (!cartItems.Any())
            {
                return RedirectToAction("Index");
            }

            var orderVM = new OrderVM
            {
                UserId = user.UserId,
                OrderDate = DateTime.Now,
                TotalAmount = cartItems.Sum(c => c.Item.Price * c.Quantity),
                DeliveryMethodId = deliveryMethodId,
                PaymentMethodId = paymentMethodId,
                SelectedAddress = deliveryMethodId == 1 ? "Pickup" : selectedAddress, // If pickup, no address needed
                OrderItems = cartItems.Select(c => new OrderItemVM
                {
                    ItemId = c.Item.ItemId,
                    Details = c.Customization,
                    Size = c.Size,
                    Quantity = c.Quantity
                }).ToList()
            };

            _orderRepo.SaveOrder(orderVM);
            _cartService.ClearCart();// Clear cart after order completion

            return RedirectToAction("Index");
        }

        // OrderSummaryController (Adding Order History Page)
        public IActionResult OrderHistory()
        {
            string userEmail = User.Identity?.Name;
            User user = _userRepo.GetUser(userEmail);
            var orders = _orderRepo.GetOrderByUserId(user.UserId);
            return View(orders);
        }
    }
}
