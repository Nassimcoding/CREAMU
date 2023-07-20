using System;
using System.Collections.Generic;

namespace CreamuAPI.Models;

public partial class PostAddress
{
    public int AddressId { get; set; }

    public int? MemberId { get; set; }

    public string? AddressType { get; set; }

    public string? RecipientName { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? PostalCode { get; set; }

    public string? PhoneNumber { get; set; }

    public ulong? IsDefault { get; set; }

    public string? Notes { get; set; }

    public virtual Member? Member { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
