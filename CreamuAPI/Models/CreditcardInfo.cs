using System;
using System.Collections.Generic;

namespace CreamuAPI.Models;

public partial class CreditcardInfo
{
    public int CreditCardId { get; set; }

    public int? MemberId { get; set; }

    public string? CardholderName { get; set; }

    public string? CardNumber { get; set; }

    public DateOnly? ExpirationDate { get; set; }

    public string? Cvv { get; set; }

    public string? CardType { get; set; }

    public ulong? IsDefault { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public string? Notes { get; set; }

    public virtual Member? Member { get; set; }
}
