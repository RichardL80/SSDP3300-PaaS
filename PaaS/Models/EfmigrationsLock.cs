using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaaS.Models;

public partial class EfmigrationsLock
{
    [Key]
    public int Id { get; set; }

    public string Timestamp { get; set; } = null!;
}
