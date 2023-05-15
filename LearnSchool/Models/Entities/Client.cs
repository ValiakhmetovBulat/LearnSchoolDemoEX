using System;
using System.Collections.Generic;

namespace LearnSchool.Models.Entities;

public partial class Client
{
    public int ClientId { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int GenderId { get; set; }

    public DateTime RegistrationDate { get; set; }

    public byte[]? ClientImage { get; set; }

    public int TagId { get; set; }

    public virtual ICollection<ClientService> ClientServices { get; set; } = new List<ClientService>();

    public virtual Gender Gender { get; set; } = null!;

    public virtual ClientTag Tag { get; set; } = null!;
}
