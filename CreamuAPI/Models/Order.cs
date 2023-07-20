using System;
using System.Collections.Generic;

namespace CreamuAPI.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? EmployeeId { get; set; }

    public int? MemberId { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? TotalAmount { get; set; }

    public string? OrderStatus { get; set; }

    public string? PaymentStatus { get; set; }

    public int? ShippingAddressId { get; set; }

    public string? OrderNotes { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Member? Member { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Orderimg> Orderimgs { get; set; } = new List<Orderimg>();

    public virtual PostAddress? ShippingAddress { get; set; }
}
