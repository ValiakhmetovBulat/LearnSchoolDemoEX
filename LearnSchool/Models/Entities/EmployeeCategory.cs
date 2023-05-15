using System;
using System.Collections.Generic;

namespace LearnSchool.Models.Entities;

public partial class EmployeeCategory
{
    public int EmployeeCategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public decimal CategoryPayment { get; set; }

    public virtual ICollection<EmployeeService> EmployeeServices { get; set; } = new List<EmployeeService>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
