using System;
using System.Collections.Generic;

namespace Shopp.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Names { get; set; } = null!;

    public string? Descriptions { get; set; }

    public int? BrandsId { get; set; }

    public int? CategoriesId { get; set; }

    public int? StockQuantity { get; set; }

    public int? Price { get; set; }

    public virtual Brand? Brands { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Category? Categories { get; set; }
}
