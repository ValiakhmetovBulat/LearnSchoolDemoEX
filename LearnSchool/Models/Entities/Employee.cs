using System;
using System.Collections.Generic;

namespace LearnSchool.Models.Entities;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string PassportSeries { get; set; } = null!;

    public string PassportNum { get; set; } = null!;

    public string DivisionCode { get; set; } = null!;

    public int GenderId { get; set; }

    public decimal PaymentRatio { get; set; }

    public DateTime DateOfBirth { get; set; }

    public int EmployeeCategoryId { get; set; }

    public virtual EmployeeCategory EmployeeCategory { get; set; } = null!;

    public virtual Gender Gender { get; set; } = null!;
}
