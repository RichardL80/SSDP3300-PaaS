using PaaS.ViewModels;
using Microsoft.EntityFrameworkCore;
using PaaS.Data;

namespace PaaS.Repositories
{
    public class OrderRepo
    {
        private readonly ApplicationDbContext _context;

        public OrderRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get order summary with all details wrapped in the ViewModel
        public OrderSummaryVM GetOrderSummary(int orderId)
        {
            var order = _context.Order
                .Include(o => o.OrderItem)
                    .ThenInclude(oi => oi.Item)
                .Include(o => o.DeliveryMethod)
                .Include(o => o.PaymentMethod)
                .Include(o => o.User)
                .FirstOrDefault(o => o.OrderId == orderId);

            if (order == null)
            {
                return null;
            }

            var userContact = _context.ContactInfos
                .Include(ci => ci.City)
                .Include(ci => ci.Province)
                .FirstOrDefault(ci => ci.UserId == order.UserId);

            var viewModel = new OrderSummaryVM
            {
                Order = order,
                UserContact = userContact,
                AddressBook = _context.ContactInfos
                    .Where(ci => ci.UserId == order.UserId)
                    .ToList(),
                Subtotal = CalculateSubtotal(orderId),
                GST = CalculateGST(CalculateSubtotal(orderId)),
                PST = CalculatePST(CalculateSubtotal(orderId)),
                Total = CalculateTotal(CalculateSubtotal(orderId)),
                EstimatedDeliveryTime = "30-50 mins"
            };

            return viewModel;
        }

        // Calculate the subtotal of the order based on item quantity and price
        private decimal CalculateSubtotal(int orderId)
        {
            var orderItems = _context.OrderItem
                .Where(oi => oi.OrderId == orderId)
                .Include(oi => oi.Item)
                .ToList();
            decimal subtotal = 0;
            foreach (var orderItem in orderItems)
            {
                subtotal += orderItem.Quantity * orderItem.Item.Price;
            }
            return subtotal;
        }

        // Calculate the GST (5%)
        private decimal CalculateGST(decimal subtotal)
        {
            return subtotal * 0.05m;
        }

        // Calculate the PST (7%)
        private decimal CalculatePST(decimal subtotal)
        {
            return subtotal * 0.07m;
        }

        // Calculate the total including GST, PST, and shipping fee
        private decimal CalculateTotal(decimal subtotal)
        {
            return subtotal + CalculateGST(subtotal) + CalculatePST(subtotal) + 10.00m; // Fixed shipping fee
        }
    }
}
