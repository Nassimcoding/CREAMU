using System;
using System.Collections.Generic;

namespace CreamuAPI.Models;

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

    public virtual ICollection<Component> Components { get; set; } = new List<Component>();
}
