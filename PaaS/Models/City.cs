using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaaS.Models;

public partial class City
{
    [Key]
    public int CityId { get; set; }

    public string Name { get; set; } = null!;

    public int ProvinceId { get; set; }

    public virtual ICollection<ContactInfo> ContactInfos { get; set; } = new List<ContactInfo>();

    public virtual Province Province { get; set; } = null!;
}
