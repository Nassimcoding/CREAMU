using System;
using System.Collections.Generic;

namespace FinalProject.Data;

public partial class Material
{
    public int MaterialId { get; set; }

    public string MaterialName { get; set; } = null!;

    public int? Permissions { get; set; }

    public string? Info { get; set; }

    public int Price { get; set; }

    public DateTime AddTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public bool IsSupply { get; set; }

    public string? MtrlFileName { get; set; }
}
