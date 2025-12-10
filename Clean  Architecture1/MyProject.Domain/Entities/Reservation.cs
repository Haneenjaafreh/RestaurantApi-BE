using System;
using System.Collections.Generic;

namespace MyProject.Infrastructure.Models;

public partial class Reservation
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int CarId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual Car Car { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
