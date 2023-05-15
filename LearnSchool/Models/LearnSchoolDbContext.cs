using System;
using System.Collections.Generic;
using LearnSchool.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearnSchool.Models;

public partial class LearnSchoolDbContext : DbContext
{
    private static LearnSchoolDbContext _context;

    public static LearnSchoolDbContext GetContext()
    {
        if (_context == null)
            _context = new LearnSchoolDbContext();
        return _context;
    }

    public LearnSchoolDbContext()
    {
    }

    public LearnSchoolDbContext(DbContextOptions<LearnSchoolDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientService> ClientServices { get; set; }

    public virtual DbSet<ClientTag> ClientTags { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeCategory> EmployeeCategories { get; set; }

    public virtual DbSet<EmployeeService> EmployeeServices { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Parameter> Parameters { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductHistory> ProductHistories { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductManufacturer> ProductManufacturers { get; set; }

    public virtual DbSet<ProductParameter> ProductParameters { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceImage> ServiceImages { get; set; }

    public virtual DbSet<UnitType> UnitTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LearnSchoolDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Client");

            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.RegistrationDate).HasColumnType("date");
            entity.Property(e => e.Surname).HasMaxLength(50);

            entity.HasOne(d => d.Gender).WithMany(p => p.Clients)
                .HasForeignKey(d => d.GenderId)
                .HasConstraintName("FK_Client_Gender");

            entity.HasOne(d => d.Tag).WithMany(p => p.Clients)
                .HasForeignKey(d => d.TagId)
                .HasConstraintName("FK_Client_ClientTag");
        });

        modelBuilder.Entity<ClientService>(entity =>
        {
            entity.ToTable("ClientService");

            entity.Property(e => e.DateOfService).HasColumnType("datetime");

            entity.HasOne(d => d.Client).WithMany(p => p.ClientServices)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK_ClientService_Client");

            entity.HasOne(d => d.Service).WithMany(p => p.ClientServices)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK_ClientService_Service");
        });

        modelBuilder.Entity<ClientTag>(entity =>
        {
            entity.ToTable("ClientTag");

            entity.Property(e => e.TagHashColor).HasMaxLength(10);
            entity.Property(e => e.TagName).HasMaxLength(50);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.DivisionCode).HasMaxLength(20);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PassportNum)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.PassportSeries).HasMaxLength(10);
            entity.Property(e => e.PaymentRatio).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Surname).HasMaxLength(50);

            entity.HasOne(d => d.EmployeeCategory).WithMany(p => p.Employees)
                .HasForeignKey(d => d.EmployeeCategoryId)
                .HasConstraintName("FK_Employee_EmployeeCategory");

            entity.HasOne(d => d.Gender).WithMany(p => p.Employees)
                .HasForeignKey(d => d.GenderId)
                .HasConstraintName("FK_Employee_Gender");
        });

        modelBuilder.Entity<EmployeeCategory>(entity =>
        {
            entity.ToTable("EmployeeCategory");

            entity.Property(e => e.CategoryName).HasMaxLength(50);
            entity.Property(e => e.CategoryPayment).HasColumnType("decimal(19, 4)");
        });

        modelBuilder.Entity<EmployeeService>(entity =>
        {
            entity.ToTable("EmployeeService");

            entity.HasOne(d => d.EmployeeCategory).WithMany(p => p.EmployeeServices)
                .HasForeignKey(d => d.EmployeeCategoryId)
                .HasConstraintName("FK_EmployeeService_EmployeeCategory");

            entity.HasOne(d => d.Service).WithMany(p => p.EmployeeServices)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK_EmployeeService_Service");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.ToTable("Gender");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Parameter>(entity =>
        {
            entity.ToTable("Parameter");

            entity.Property(e => e.ParameterName).HasMaxLength(50);

            entity.HasOne(d => d.UnitType).WithMany(p => p.Parameters)
                .HasForeignKey(d => d.UnitTypeId)
                .HasConstraintName("FK_Parameter_UnitType");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.ProductCost).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.ProductName).HasMaxLength(50);

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategoryId)
                .HasConstraintName("FK_Product_ProductCategory");

            entity.HasOne(d => d.ProductManufacturer).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductManufacturerId)
                .HasConstraintName("FK_Product_ProductManufacturer");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.ToTable("ProductCategory");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ProductHistory>(entity =>
        {
            entity.ToTable("ProductHistory");

            entity.Property(e => e.BuyDate).HasColumnType("date");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductHistories)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductHistory_Product");

            entity.HasOne(d => d.Service).WithMany(p => p.ProductHistories)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK_ProductHistory_Service");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.ToTable("ProductImage");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductImage_Product");
        });

        modelBuilder.Entity<ProductManufacturer>(entity =>
        {
            entity.ToTable("ProductManufacturer");

            entity.Property(e => e.DateOfBeggining).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ProductParameter>(entity =>
        {
            entity.HasKey(e => e.ProductParameter1);

            entity.ToTable("ProductParameter");

            entity.Property(e => e.ProductParameter1).HasColumnName("ProductParameter");
            entity.Property(e => e.Value).HasMaxLength(50);

            entity.HasOne(d => d.Parameter).WithMany(p => p.ProductParameters)
                .HasForeignKey(d => d.ParameterId)
                .HasConstraintName("FK_ProductParameter_Parameter");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductParameters)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductParameter_Product");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.ToTable("Service");

            entity.Property(e => e.ServiceCost).HasColumnType("decimal(19, 4)");
        });

        modelBuilder.Entity<ServiceImage>(entity =>
        {
            entity.ToTable("ServiceImage");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceImages)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK_ServiceImage_Service");
        });

        modelBuilder.Entity<UnitType>(entity =>
        {
            entity.ToTable("UnitType");

            entity.Property(e => e.UnitTypeName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
