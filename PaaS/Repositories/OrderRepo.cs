using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using PaaS.Data;
using PaaS.ViewModels;
using PaaS.Data;
using PaaS.Models;
using PaaS.ViewModels;

namespace PaaS.Repositories
{
    public class OrderRepo
    {
        private readonly ApplicationDbContext _context;

        public OrderRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SaveOrder(OrderVM orderVM)
        {
            var order = new Order
            {
                UserId = orderVM.UserId,
                OrderDate = DateTime.Now,
                TotalAmount = orderVM.TotalAmount,
                DeliveryMethodId = orderVM.DeliveryMethodId,
                PaymentMethodId = orderVM.PaymentMethodId,
                StatusId = 1 // Assuming 1 = 'Pending'
            };

            _context.Order.Add(order);
            _context.SaveChanges();

            foreach (var item in orderVM.OrderItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.OrderId,
                    ItemId = item.ItemId,
                    Details = item.Details,
                    Size = item.Size,
                    Quantity = item.Quantity
                };
                _context.OrderItem.Add(orderItem);
            }
            _context.SaveChanges();
        }

        public List<OrderVM> GetOrderByUserId(int userId)
        {
            return _context.Order
                .Where(o => o.UserId == userId)
                .Select(o => new OrderVM
                {
                    OrderId = o.OrderId,
                    UserId = o.UserId,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    DeliveryMethodId = o.DeliveryMethodId,
                    PaymentMethodId = o.PaymentMethodId,
                    StatusId = o.StatusId,
                    OrderItems = o.OrderItem.Select(oi => new OrderItemVM
                    {
                        ItemId = oi.ItemId,
                        Details = oi.Details,
                        Size = oi.Size,
                        Quantity = oi.Quantity
                    }).ToList()
                }).ToList();
        }
    }
}
