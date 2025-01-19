using System;
using System.Collections.Generic;

namespace PaaS.Models;

public partial class ItemType
{
    public int ItemTypeId { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
