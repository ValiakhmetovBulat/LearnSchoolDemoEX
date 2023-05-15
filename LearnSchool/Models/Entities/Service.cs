using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace LearnSchool.Models.Entities;

public partial class Service
{
    public int ServiceId { get; set; }

    public string ServiceName { get; set; } = null!;

    public string? ServiceDescription { get; set; }

    public decimal ServiceCost { get; set; }

    public int ServiceDiscount { get; set; }

    public int ServiceDuration { get; set; }

    public string? ServiceMainImage { get; set; }

    public virtual ICollection<ClientService> ClientServices { get; set; } = new List<ClientService>();

    public virtual ICollection<EmployeeService> EmployeeServices { get; set; } = new List<EmployeeService>();

    public virtual ICollection<ProductHistory> ProductHistories { get; set; } = new List<ProductHistory>();

    public virtual ICollection<ServiceImage> ServiceImages { get; set; } = new List<ServiceImage>();

    [NotMapped]
    public string ServiceImageFromResources => "/Resources/" + ServiceMainImage;

    [NotMapped]
    public string ServiceCostWithDiscountText => ServiceDiscount == 0 ? "" : " " + ServiceCostWithDiscount.ToString();

    [NotMapped]
    public decimal ServiceCostWithDiscount => ServiceDiscount == 0 ? ServiceCost : (ServiceCost - ((ServiceCost / 100) * ServiceDiscount));

    [NotMapped]
    public string ServiceCostTextDecoration => ServiceDiscount == 0 ? "None" : "Strikethrough";

    [NotMapped]
    public string ServiceDiscountText => ServiceDiscount == 0 ? "" : $"* скидка {ServiceDiscount}%";

    [NotMapped]
    public SolidColorBrush ServiceDiscountBackgroundColor => ServiceDiscount > 0 ? new SolidColorBrush(Colors.LightGreen) : new SolidColorBrush(Colors.White);
}
