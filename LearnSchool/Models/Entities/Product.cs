using System;
using System.Collections.Generic;

namespace LearnSchool.Models.Entities;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int ProductCategoryId { get; set; }

    public decimal ProductCost { get; set; }

    public int ProductManufacturerId { get; set; }

    public byte[] ProductMainImage { get; set; } = null!;

    public bool IsActual { get; set; }

    public virtual ProductCategory ProductCategory { get; set; } = null!;

    public virtual ICollection<ProductHistory> ProductHistories { get; set; } = new List<ProductHistory>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ProductManufacturer ProductManufacturer { get; set; } = null!;

    public virtual ICollection<ProductParameter> ProductParameters { get; set; } = new List<ProductParameter>();
}
