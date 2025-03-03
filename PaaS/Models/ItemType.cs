using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaaS.Models;

public class ItemType
{
    [Key]
    public int ItemTypeId { get; set; }

    public string Description { get; set; } = null!;
}
