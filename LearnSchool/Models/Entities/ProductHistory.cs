using System;
using System.Collections.Generic;

namespace LearnSchool.Models.Entities;

public partial class ProductHistory
{
    public int ProductHistoryId { get; set; }

    public DateTime BuyDate { get; set; }

    public int Count { get; set; }

    public int ServiceId { get; set; }

    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
