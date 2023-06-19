using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Data;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? Name { get; set; }

    public string? Telephone { get; set; }

    public string? Password { get; set; }

    public int? EmailId { get; set; }

    public string? Address { get; set; }

    public string? Image { get; set; }

    [Display(Name = "Birthday")]
    [DataType(DataType.Date)]
    
    public DateTime? Birthday { get; set; }

    public string? Title { get; set; }

    [DataType(DataType.Date)]
    public DateTime? JoinDate { get; set; }

    public string? Notes { get; set; }

    public virtual Account? Email { get; set; }
}
