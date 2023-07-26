using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Data;

public partial class Product
{
    internal int SortOrder;

    [DisplayName("圖片")]
    //[Required(ErrorMessage = "請上傳商品圖片")]
    //[AllowedImageExtensions(ErrorMessage = "圖片格式不正確，只支援 JPG、PNG、GIF 格式。")]
    public string? ProductImage { get; set; }

    public int ProductId { get; set; }

    [DisplayName("名稱")]
    [Required(ErrorMessage = "請輸入商品名稱")]
    [MinLength(3, ErrorMessage = "商品名稱要超過3個字以上")]
    public string? ProductName { get; set; }

    [DisplayName("描述")]
    [Required(ErrorMessage = "請輸入商品敘述")]
    [MinLength(5, ErrorMessage = "商品敘述要超過5個字以上")]
    [MaxLength(500, ErrorMessage = "商品敘述不能超過300個字")]
    public string? Descript { get; set; }

    [DisplayName("售價")]
    [Required(ErrorMessage = "請輸入商品售價")]
    [Range(100, int.MaxValue, ErrorMessage = "商品售價必須大於100元")]
    public int? Price { get; set; }

    [DisplayName("狀態")]
    public string? ProductStatus { get; set; }

    [DisplayName("庫存")]
    [Required(ErrorMessage = "請輸入庫存數量")]
    public int? ProductStock { get; set; }

    [DisplayName("上架日期")]
    public string? ReleaseDate { get; set; }

    [DisplayName("更新日期")]
    public string? UpdatedDate { get; set; }

    [DisplayName("類型")]
    public string? Type { get; set; }

    [DisplayName("類別")]
    public int? CategoryId { get; set; } // 這是外鍵欄位


    //新增
    //public Category Category { get; set; } // 導航屬性，用於關聯到 Category 資料表

}
