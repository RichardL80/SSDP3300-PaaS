using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaaS.Models;

public partial class OrderItem
{
    [Key]
    public int OrderId { get; set; }

    public int ItemId { get; set; }

    public string? Details { get; set; }

    public string? Size { get; set; }

    public int Quantity { get; set; }

    public virtual Item Item { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
