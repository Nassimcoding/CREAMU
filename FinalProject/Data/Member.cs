using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Data;

public partial class Member
{
    public int MemberId { get; set; }

    [Required(ErrorMessage = "此欄位必填 !")]
    [Display(Name = "姓名")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "此欄位必填 !")]
    [Display(Name = "電話")]
    public string? Telephone { get; set; }

    [Required(ErrorMessage = "此欄位必填 !")]
    [Display(Name = "密碼")]
    public string? Password { get; set; }

    
    public int? EmailId { get; set; }

    [Required(ErrorMessage = "此欄位必填 !")]
    [Display(Name = "地址")]
    public string? Address { get; set; }

    [Required(ErrorMessage = "此欄位必填 !")]
    [Display(Name = "地址")]
    public DateTime? Birthday { get; set; }

    [Required(ErrorMessage = "此欄位必填 !")]
    [Display(Name = "權限")]
    public int? Level { get; set; }

    [Required(ErrorMessage = "此欄位必填 !")]
    [Display(Name = "加入日期")]
    public DateTime? JoinDate { get; set; }

    [Required(ErrorMessage = "此欄位必填 !")]
    [Display(Name = "照片")]
    public string? Image { get; set; }

    
    [Display(Name = "備註")]
    public string? Notes { get; set; }

    public virtual ICollection<CreditcardInfo> CreditcardInfos { get; set; } = new List<CreditcardInfo>();

    public virtual Account? Email { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<PostAddress> PostAddresses { get; set; } = new List<PostAddress>();

    public virtual ICollection<TempOrderDetail> TempOrderDetails { get; set; } = new List<TempOrderDetail>();

    public virtual ICollection<TrackingList> TrackingLists { get; set; } = new List<TrackingList>();
}
