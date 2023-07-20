using System;
using System.Collections.Generic;

namespace CreamuAPI.Models;

public partial class Model
{
    public int ModelId { get; set; }

    public string ModelName { get; set; } = null!;

    public string ModelType { get; set; } = null!;

    public int? Permissions { get; set; }

    public string? Info { get; set; }

    public int Price { get; set; }

    public DateTime AddTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public bool IsSupply { get; set; }

    public string? ModelFileName { get; set; }

    public virtual ICollection<Component> Components { get; set; } = new List<Component>();
}
