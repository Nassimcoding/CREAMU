using System;
using System.Collections.Generic;

namespace CreamuAPI.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? Descript { get; set; }

    public int? CategoryId { get; set; }

    public int? Price { get; set; }

    public string? ProductStatus { get; set; }

    public int? ProductStock { get; set; }

    public string? ProductImage { get; set; }

    public string? ReleaseDate { get; set; }

    public string? UpdatedDate { get; set; }

    public string? Type { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<TempOrderDetail> TempOrderDetails { get; set; } = new List<TempOrderDetail>();

    public virtual ICollection<TrackingList> TrackingLists { get; set; } = new List<TrackingList>();
}
