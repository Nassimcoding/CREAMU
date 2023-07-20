using System;
using System.Collections.Generic;

namespace CreamuAPI.Models;

public partial class Account
{
    public int EmailId { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();
}
