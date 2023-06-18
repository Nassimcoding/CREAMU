using System;
using System.Collections.Generic;

namespace FinalProject.Data;

public partial class Component
{
    public int ComponentId { get; set; }

    public int? ModelId { get; set; }

    public string? ModelType { get; set; }

    public int? MaterialId { get; set; }

    public int? StockQty { get; set; }

    public int? SoldQty { get; set; }

    public bool? IsSupply { get; set; }

    public string? ComFileName { get; set; }
}
