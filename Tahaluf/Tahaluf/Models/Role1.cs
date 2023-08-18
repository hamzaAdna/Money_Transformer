using System;
using System.Collections.Generic;

namespace Tahaluf.Models;

public partial class Role1
{
    public decimal Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Useracount> Useracounts { get; set; } = new List<Useracount>();
}
