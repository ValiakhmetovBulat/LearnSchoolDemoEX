using System;
using System.Collections.Generic;

namespace LearnSchool.Models.Entities;

public partial class ProductParameter
{
    public int ProductParameter1 { get; set; }

    public int ProductId { get; set; }

    public int ParameterId { get; set; }

    public string Value { get; set; } = null!;

    public virtual Parameter Parameter { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
