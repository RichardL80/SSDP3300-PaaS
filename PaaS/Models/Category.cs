using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaaS.Models;

public partial class Category
{
    [Key]
    public int IdCategory { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
