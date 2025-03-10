using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PaaS.Models;
using PaaS.ViewModels;

namespace PaaS.ViewModels
{
    public class OrderVM
    {

        public int OrderId { get; set; }

        public int UserId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public int DeliveryMethodId { get; set; }

        public int PaymentMethodId { get; set; }

        public int StatusId { get; set; }

        public string? SelectedAddress { get; set; } // Store selected delivery address
        public List<OrderItemVM> OrderItems { get; set; } = new List<OrderItemVM>();
    }
}