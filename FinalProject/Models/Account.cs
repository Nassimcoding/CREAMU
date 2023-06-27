using System;
using System.Collections.Generic;

namespace Product_new.Models
{
    public partial class Account
    {
        public Account()
        {
            Employees = new HashSet<Employee>();
            Members = new HashSet<Member>();
        }

        public int EmailId { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Member> Members { get; set; }
    }
}
