using System;
using System.Collections.Generic;

namespace Shopp.Models;

public partial class Category
{
    public int CategoriesId { get; set; }

    public string Names { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
