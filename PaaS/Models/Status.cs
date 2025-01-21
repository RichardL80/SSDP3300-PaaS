using System;
using System.Collections.Generic;

namespace PaaS.Models;

public partial class Status
{
    public int StatusId { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
