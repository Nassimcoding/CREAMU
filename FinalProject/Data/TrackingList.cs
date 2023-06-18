using System;
using System.Collections.Generic;

namespace FinalProject.Data;

public partial class TrackingList
{
    public int MemberId { get; set; }

    public int ProductId { get; set; }

    public string? TrackingDate { get; set; }
}
