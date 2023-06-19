using System;
using System.Collections.Generic;

namespace FinalProject.Data;

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
}
