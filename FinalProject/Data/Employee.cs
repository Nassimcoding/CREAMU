using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Data;

public partial class Employee
{
    public int EmployeeId { get; set; }

    [Required(ErrorMessage ="此欄位必填!")]
    [Display(Name ="姓名")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "此欄位必填!")]
    [Display(Name = "電話")]
    public string? Telephone { get; set; }

    [Required(ErrorMessage = "此欄位必填!")]
    [Display(Name = "密碼")]
    public string? Password { get; set; }

    public int? EmailId { get; set; }

    [Required(ErrorMessage = "此欄位必填!")]
    [Display(Name = "地址")]
    public string? Address { get; set; }

    
    [Display(Name = "照片")]
    public string? Image { get; set; }

    [Required(ErrorMessage = "此欄位必填!")]
    [Display(Name = "生日")]
    public DateTime? Birthday { get; set; }

    [Required(ErrorMessage = "此欄位必填!")]
    [Display(Name = "職稱")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "此欄位必填!")]
    [Display(Name = "到職日")]
    public DateTime? JoinDate { get; set; }

    
    [Display(Name = "備註")]
    public string? Notes { get; set; }

    public virtual Account? Email { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
