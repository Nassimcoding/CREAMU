using System;
using System.Collections.Generic;

namespace CreamuAPI.Models;

public partial class Member
{
    public int MemberId { get; set; }

    public string? Name { get; set; }

    public string? Telephone { get; set; }

    public string? Password { get; set; }

    public int? EmailId { get; set; }

    public string? Address { get; set; }

    public DateTime? Birthday { get; set; }

    public int? Level { get; set; }

    public DateTime? JoinDate { get; set; }

    public string? Image { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<CreditcardInfo> CreditcardInfos { get; set; } = new List<CreditcardInfo>();

    public virtual Account? Email { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<PostAddress> PostAddresses { get; set; } = new List<PostAddress>();

    public virtual ICollection<TempOrderDetail> TempOrderDetails { get; set; } = new List<TempOrderDetail>();

    public virtual ICollection<TrackingList> TrackingLists { get; set; } = new List<TrackingList>();
}
