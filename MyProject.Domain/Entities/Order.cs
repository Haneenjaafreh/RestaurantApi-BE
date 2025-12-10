using System;
using System.Collections.Generic;

namespace MyProject.Infrastructure.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int TableNumber { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
