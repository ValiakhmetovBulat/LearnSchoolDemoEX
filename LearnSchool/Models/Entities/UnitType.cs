using System;
using System.Collections.Generic;

namespace LearnSchool.Models.Entities;

public partial class UnitType
{
    public int UnitTypeId { get; set; }

    public string UnitTypeName { get; set; } = null!;

    public virtual ICollection<Parameter> Parameters { get; set; } = new List<Parameter>();
}
