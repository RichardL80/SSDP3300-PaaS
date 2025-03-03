using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaaS.Models;

public class OrderItem
{
    [Key]
    public int OrderId { get; set; }

    public int ItemId { get; set; }

    public string? Details { get; set; }

    public string? Size { get; set; }

    public int Quantity { get; set; }

    [ForeignKey("ItemId")]
    public virtual Item Item { get; set; } = null!;

    [ForeignKey("OrderId")]
    public virtual Order Order { get; set; } = null!;
}
