using Microsoft.AspNetCore.Mvc;
using PaaS.Models;
using PaaS.Repositories;
using PaaS.ViewModels;
using Microsoft.EntityFrameworkCore;


namespace PaaS.Controllers
{
    public class OrderSummaryController : Controller
    {
        private readonly OrderRepo _orderRepository;
        private readonly ContactInfoRepo _contactInfoRepository;

        public OrderSummaryController(OrderRepo orderRepository, ContactInfoRepo contactInfoRepository)
        {
            _orderRepository = orderRepository;
            _contactInfoRepository = contactInfoRepository;
        }
        public IActionResult Index(int orderId = 1) // Default to orderId=1 if not provided
        {
            // Create a mock OrderSummaryVM to test the view
            var orderSummary = new OrderSummaryVM
            {
                Order = new Order
                {
                    OrderItem = new List<OrderItem>
                {
                    new OrderItem { Quantity = 1, Size ="Medium", Item = new Item { Price = 12.99m ,Name ="Peperoni"} },
                    new OrderItem { Quantity = 2, Size ="Large", Item = new Item { Price = 32.50m , Name ="Meet Lover"} }
                }
                },
                AddressBook = new List<ContactInfo>
            {
                new ContactInfo { Address1=" 1000 No.3 Road, Richmond A2k 3N4", Address2 = "2000 Oak Rd, Vancouver V1K 1P7"}
            },
                ShippingFee = 10.00m,
                EstimatedDeliveryTime = "30-50 mins"
            };


            // Calculate the subtotal
            orderSummary.Subtotal = orderSummary.Order.OrderItem.Sum(item => item.Quantity * item.Item.Price);

            // Assume 5% GST and 7% PST
            orderSummary.GST = Math.Round(orderSummary.Subtotal * 0.05m, 2);
            orderSummary.PST = Math.Round(orderSummary.Subtotal * 0.07m, 2);

            // Calculate the total
            orderSummary.Total = Math.Round(orderSummary.Subtotal + orderSummary.GST + orderSummary.PST + orderSummary.ShippingFee, 2);

            // Estimated delivery time (keeping it fixed)
            orderSummary.EstimatedDeliveryTime = "30-50 mins";

            return View(orderSummary); // Pass mock data to the view
        }

        // Place Order action
        [HttpPost]
        public IActionResult PlaceOrder(int orderId, int selectedAddressId)
        {
            var orderSummary = _orderRepository.GetOrderSummary(orderId);
            if (orderSummary == null)
            {
                return NotFound();
            }

            // Update the delivery address if selected
            var contactInfo = _contactInfoRepository.GetContactInfoById(selectedAddressId);
            if (contactInfo != null)
            {
                orderSummary.UserContact = contactInfo;
            }

            // Logic to process the order
            return RedirectToAction("Index", "PayPal", new { orderId = orderId });

        }
    }
}
