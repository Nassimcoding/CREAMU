using System;
using System.Collections.Generic;

namespace CreamuAPI.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public int? Qty { get; set; }

    public int? UnitPrice { get; set; }

    public float? Discount { get; set; }

    public int? Subtotal { get; set; }

    public string? Notes { get; set; }

    public string? Type { get; set; }

    public virtual Order? Order { get; set; }
}
