using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Media;

namespace LearnSchool.Models.Entities;

public partial class ClientService
{
    public int ClientServiceId { get; set; }

    public int ClientId { get; set; }

    public int ServiceId { get; set; }

    public DateTime DateOfService { get; set; } 

    public virtual Client Client { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;

    [NotMapped]
    public int MinutesToBegin => Convert.ToInt32(DateOfService.Subtract(DateTime.Now).TotalMinutes);

    [NotMapped]
    public string StringTimeToBegin => (MinutesToBegin / 60) + " часов " + (MinutesToBegin % 60) + " минут до начала";

    [NotMapped]
    public SolidColorBrush CleintServiceEstimatingTime => MinutesToBegin <= 60 ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.White);
}
