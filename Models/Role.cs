﻿using System;
using System.Collections.Generic;

namespace PaaS.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
