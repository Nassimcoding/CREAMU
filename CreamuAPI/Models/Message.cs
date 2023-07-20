using System;
using System.Collections.Generic;

namespace CreamuAPI.Models;

public partial class Message
{
    public int MessageId { get; set; }

    public int? MemberId { get; set; }

    public int? EmployeeId { get; set; }

    public int Score { get; set; }

    public DateTime MessageTime { get; set; }

    public DateTime? ReplyTime { get; set; }

    public string MessageContext { get; set; } = null!;

    public string? ReplyContext { get; set; }

    public bool IsReply { get; set; }

    public bool IsShow { get; set; }

    public string? Image { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Member? Member { get; set; }
}
