using System;
using System.Collections.Generic;

namespace LearnSchool.Models.Entities;

public partial class EmployeeService
{
    public int EmployeeServiceId { get; set; }

    public int EmployeeCategoryId { get; set; }

    public int ServiceId { get; set; }

    public bool IsSameGenderRequired { get; set; }

    public virtual EmployeeCategory EmployeeCategory { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
