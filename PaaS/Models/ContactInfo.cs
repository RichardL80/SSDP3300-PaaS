using System;
using System.Collections.Generic;

namespace PaaS.Models;

public partial class ContactInfo
{
    public int ContactId { get; set; }

    public string Phone { get; set; } = null!;

    public string Address1 { get; set; } = null!;

    public string? Address2 { get; set; }

    public int CityId { get; set; }

    public int ProvinceId { get; set; }

    public int UserId { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual Province Province { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
