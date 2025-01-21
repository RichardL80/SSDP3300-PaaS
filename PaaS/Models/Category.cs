using System;
using System.Collections.Generic;

namespace PaaS.Models;

public partial class Category
{
    public int IdCategory { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
