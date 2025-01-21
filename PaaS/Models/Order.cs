using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaaS.Models;

public partial class Order
{
    [Key]
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public int DeliveryMethodId { get; set; }

    public int PaymentMethodId { get; set; }

    public int StatusId { get; set; }

    public virtual DeliveryMethod DeliveryMethod { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual PaymentMethod PaymentMethod { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
