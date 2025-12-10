using System;
using System.Collections.Generic;

namespace MyProject.Infrastructure.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string Name { get; set; } = null!;

    public string Position { get; set; } = null!;
    public string Email { get; set; }
    public string Password { get; set; }
}
