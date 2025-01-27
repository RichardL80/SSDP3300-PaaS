using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PaaS.ViewModels;

public class ItemVM
{
    [DisplayName ("Item Id")]
    public int ItemId { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    [Required]
    [DataType(DataType.Currency)]
    [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Please enter a valid price.")]
    public decimal Price { get; set; }

    [DisplayName ("Image URL")]
    public string? ImgUrl { get; set; }

    [Required]
    public int ItemTypeId { get; set; }
    
    [DisplayName ("Item Type")]
    public string? ItemTypeDescription { get; set; } = null!;

    [Required]
    public int IdCategory { get; set; }
    
    [DisplayName ("Category")]
    public string? CategoryDescription { get; set; } = null!;
}