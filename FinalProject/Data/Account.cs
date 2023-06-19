using System;
using System.Collections.Generic;

namespace FinalProject.Data;

public partial class Account
{
    public int EmailId { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();
}
