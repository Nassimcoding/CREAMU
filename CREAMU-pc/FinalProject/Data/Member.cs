using System;
using System.Collections.Generic;

namespace FinalProject.Data;

public partial class Member
{
    public int MemberId { get; set; }

    public string? Name { get; set; }

    public string? Telephone { get; set; }

    public string? Password { get; set; }

    public int? EmailId { get; set; }

    public string? Address { get; set; }

    public DateOnly? Birthday { get; set; }

    public int? Level { get; set; }

    public DateTime? JoinDate { get; set; }

    public string? Image { get; set; }

    public string? Notes { get; set; }

    public virtual Account? Email { get; set; }
}
