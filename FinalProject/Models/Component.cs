using System;
using System.Collections.Generic;

namespace Product_new.Models
{
    public partial class Component
    {
        public Component()
        {
            CombineDetailCbodyNavigations = new HashSet<CombineDetail>();
            CombineDetailCheadNavigations = new HashSet<CombineDetail>();
            CombineDetailClfootNavigations = new HashSet<CombineDetail>();
            CombineDetailClhandNavigations = new HashSet<CombineDetail>();
            CombineDetailCrfootNavigations = new HashSet<CombineDetail>();
            CombineDetailCrhandNavigations = new HashSet<CombineDetail>();
        }

        public int ComponentId { get; set; }
        public int? ModelId { get; set; }
        public string? ModelType { get; set; }
        public int? MaterialId { get; set; }
        public int? StockQty { get; set; }
        public int? SoldQty { get; set; }
        public bool? IsSupply { get; set; }
        public string? ComFileName { get; set; }

        public virtual Material? Material { get; set; }
        public virtual Model? Model { get; set; }
        public virtual ICollection<CombineDetail> CombineDetailCbodyNavigations { get; set; }
        public virtual ICollection<CombineDetail> CombineDetailCheadNavigations { get; set; }
        public virtual ICollection<CombineDetail> CombineDetailClfootNavigations { get; set; }
        public virtual ICollection<CombineDetail> CombineDetailClhandNavigations { get; set; }
        public virtual ICollection<CombineDetail> CombineDetailCrfootNavigations { get; set; }
        public virtual ICollection<CombineDetail> CombineDetailCrhandNavigations { get; set; }
    }
}
