using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FinalProject.Data;

public partial class Product
{
    internal int SortOrder;

    [DisplayName("商品圖")]
    public string? ProductImage { get; set; }

    public int ProductId { get; set; }

    [DisplayName("名稱")]
    //[Required(ErrorMessage = "請輸入商品名稱")]
    public string? ProductName { get; set; }

    [DisplayName("敘述")]
    public string? Descript { get; set; }

    [DisplayName("類型")]
    public int? CategoryId { get; set; }

    [DisplayName("售價")]
    public int? Price { get; set; }

    [DisplayName("狀態")]
    public string? ProductStatus { get; set; }

    [DisplayName("庫存")]
    public int? ProductStock { get; set; }

    [DisplayName("上架日期")]
    public string? ReleaseDate { get; set; }

    [DisplayName("更新日期")]
    public string? UpdatedDate { get; set; }

    [DisplayName("類型")]
    public string? Type { get; set; }


}
