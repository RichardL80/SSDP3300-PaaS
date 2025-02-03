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
            var mockOrderSummary = new OrderSummaryVM
            {
                Order = new Order
                {
                    OrderItem = new List<OrderItem>
                {
                    new OrderItem { Quantity = 1, Size ="Medium", Item = new Item { Price = 12.99m ,Name ="Peperoni"} },
                    new OrderItem { Quantity = 2, Size ="Large", Item = new Item { Price = 32.50m , Name ="Meet Lover"} }
                }
                },
                UserContact = new ContactInfo { Address1 = "123 Main St, Vancouver K2N 7D4" },
                AddressBook = new List<ContactInfo>
            {
                new ContactInfo { Address1 = "456 Oak Rd, Vancouver V1K 1P7"}
            },
                Subtotal = 45.49m,
                GST = 2.27m,
                PST = 3.18m,
                ShippingFee = 10.00m,
                Total = 60.94m,
                EstimatedDeliveryTime = "30-50 mins"
            };

            return View(mockOrderSummary); // Pass mock data to the view
        }
        // Order Summary View
        // public IActionResult Index(int orderId)
        // {
        //     // Get order details by order ID
        //     var orderSummary = _orderRepository.GetOrderSummary(orderId); // Getting the order view model (OrderSummaryVM)
        //     if (orderSummary == null)
        //     {
        //         return NotFound();
        //     }

        //     // Get the user contact information to display the address
        //     var userContact = _contactInfoRepository.GetUserContactInfo(orderSummary.UserContact.UserId);

        //     // Calculate the required values
        //     var subtotal = CalculateSubtotal(orderSummary.Order); // Calculate subtotal using the helper method
        //     var gst = CalculateGST(subtotal); // Calculate GST
        //     var pst = CalculatePST(subtotal); // Calculate PST
        //     var total = CalculateTotal(subtotal); // Calculate the total

        //     var viewModel = new OrderSummaryVM
        //     {
        //         Order = orderSummary.Order, // Pass the order
        //         UserContact = userContact, // Pass the user's contact info
        //         AddressBook = _contactInfoRepository.GetUserAddressBook(orderSummary.UserContact.UserId), // Get the address book
        //         Subtotal = subtotal, // Set the subtotal
        //         GST = gst, // Set the GST
        //         PST = pst, // Set the PST
        //         ShippingFee = 10.00m, // Fixed shipping fee
        //         Total = total, // Set the total
        //         EstimatedDeliveryTime = "30-50 mins" // Set the estimated delivery time
        //     };

        //     return View(viewModel);
        // }

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

            // Logic to process the order (e.g., redirect to PayPal)
            return Redirect("https://www.paypal.com");
        }

        // Helper methods for calculations
        private decimal CalculateSubtotal(Order order)
        {
            decimal subtotal = 0;
            foreach (var orderItem in order.OrderItem)
            {
                subtotal += orderItem.Quantity * orderItem.Item.Price;
            }
            return subtotal;
        }

        private decimal CalculateGST(decimal subtotal) => subtotal * 0.05m;

        private decimal CalculatePST(decimal subtotal) => subtotal * 0.07m;

        private decimal CalculateTotal(decimal subtotal) => subtotal + CalculateGST(subtotal) + CalculatePST(subtotal) + 10.00m;
    }
}
