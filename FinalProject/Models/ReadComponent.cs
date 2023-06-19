using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FinalProject.Data;

namespace FinalProject.Data
{
    public partial class ReadComponent
    {
        [Key]
        public int ComponentId { get; set; }
        public int? ModelId { get; set; }
        public string? ModelType { get; set; }
        public int? MaterialId { get; set; }
        public int? StockQty { get; set; }
        public int? SoldQty { get; set; }
        public bool? IsSupply { get; set; }
        public string? ComFileName { get; set; }
    }
}
