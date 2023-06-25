using System;
using System.Collections.Generic;

namespace FinalProject.Data;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? Category1 { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
