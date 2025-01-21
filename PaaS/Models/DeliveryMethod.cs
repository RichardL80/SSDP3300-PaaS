using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaaS.Models;

public partial class DeliveryMethod
{
    [Key]
    public int DeliveryMethodId { get; set; }

    public string MethodName { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
