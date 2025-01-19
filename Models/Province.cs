using System;
using System.Collections.Generic;

namespace PaaS.Models;

public partial class Province
{
    public int ProvinceId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual ICollection<ContactInfo> ContactInfos { get; set; } = new List<ContactInfo>();
}
