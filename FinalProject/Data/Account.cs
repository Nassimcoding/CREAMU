using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Data;

public partial class Account
{
    public int EmailId { get; set; }

    [Required(ErrorMessage = "Email 是必填的。")]
    [Display(Name = "Email")]
    [EmailAddress(ErrorMessage = "請輸入有效的 Email 地址。")]
    public string Email { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();
}
