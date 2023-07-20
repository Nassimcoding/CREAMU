using System;
using System.Collections.Generic;

namespace CreamuAPI.Models;

public partial class TempOrderDetail
{
    public int OrderDetailId { get; set; }

    public int? MemberId { get; set; }

    public int? ProductId { get; set; }

    public int? Qty { get; set; }

    public int? UnitPrice { get; set; }

    public float? Discount { get; set; }

    public int? Subtotal { get; set; }

    public string? Notes { get; set; }

    public virtual Member? Member { get; set; }

    public virtual Product? Product { get; set; }
}
