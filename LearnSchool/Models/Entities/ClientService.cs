using System;
using System.Collections.Generic;

namespace LearnSchool.Models.Entities;

public partial class ClientService
{
    public int ClientServiceId { get; set; }

    public int ClientId { get; set; }

    public int ServiceId { get; set; }

    public DateTime DateOfService { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
