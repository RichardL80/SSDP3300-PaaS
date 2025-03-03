
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using PaaS.Data;
using PaaS.ViewModels;
using PaaS.Models;

namespace PaaS.Repositories
{
    public class OrderRepo
    {
        private readonly ApplicationDbContext _db;

        public OrderRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<OrderVM> GetOrderByUserId(int userId)
        {
            IEnumerable<OrderVM> ordersVM = _db.Order
                .Join(_db.User,
                o => o.UserId,
                u => u.UserId,
                (o, u) => new OrderVM
                {
                    OrderId = o.OrderId,
                    UserId = o.UserId,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                }).Where(o => o.UserId == userId).ToList();
            return ordersVM;
        }
    }
}

