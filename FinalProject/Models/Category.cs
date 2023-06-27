using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Product_new.Models
{
    public partial class Category
    {
       
        [DisplayName("類別ID")]
        public int CategoryId { get; set; }


        [DisplayName("類別")]
        public string? Category1 { get; set; }


    }
}
