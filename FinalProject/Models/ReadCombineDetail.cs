using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public partial class ReadCombineDetail
    {
        [Key]
        public int CombineId { get; set; }
        public int? Chead { get; set; }
        public int? Cbody { get; set; }
        public int? Clhand { get; set; }
        public int? Crhand { get; set; }
        public int? Clfoot { get; set; }
        public int? Crfoot { get; set; }
        public int? SubTotal { get; set; }
    }
}
