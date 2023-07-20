using System;
using System.Collections.Generic;

namespace CreamuAPI.Models;

public partial class News
{
    public int NewsId { get; set; }

    public string? Title { get; set; }

    public DateOnly? Date { get; set; }

    public string? Image { get; set; }

    public string? Notes1 { get; set; }

    public string? Notes2 { get; set; }
}
