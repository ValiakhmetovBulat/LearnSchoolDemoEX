using System;
using System.Collections.Generic;

namespace LearnSchool.Models.Entities;

public partial class ServiceImage
{
    public int ServiceImageId { get; set; }

    public int ServiceId { get; set; }

    public byte[] Image { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
