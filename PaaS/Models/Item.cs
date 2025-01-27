using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaaS.Models;

public class Item
{
    [Key]
    public int ItemId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public string? ImgUrl { get; set; }

    public int ItemTypeId { get; set; }

    public int IdCategory { get; set; }
    
    [ForeignKey("ItemTypeId")]
    public ItemType ItemType { get; set; } = null!;
    
    [ForeignKey("IdCategory")]
    public Category Category { get; set; } = null!;
    
}
