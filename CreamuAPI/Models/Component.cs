using System;
using System.Collections.Generic;

namespace CreamuAPI.Models;

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

    public virtual ICollection<CombineDetail> CombineDetailCbodyNavigations { get; set; } = new List<CombineDetail>();

    public virtual ICollection<CombineDetail> CombineDetailCheadNavigations { get; set; } = new List<CombineDetail>();

    public virtual ICollection<CombineDetail> CombineDetailClfootNavigations { get; set; } = new List<CombineDetail>();

    public virtual ICollection<CombineDetail> CombineDetailClhandNavigations { get; set; } = new List<CombineDetail>();

    public virtual ICollection<CombineDetail> CombineDetailCrfootNavigations { get; set; } = new List<CombineDetail>();

    public virtual ICollection<CombineDetail> CombineDetailCrhandNavigations { get; set; } = new List<CombineDetail>();

    public virtual Material? Material { get; set; }

    public virtual Model? Model { get; set; }
}
