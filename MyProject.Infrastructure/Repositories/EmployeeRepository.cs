using Microsoft.EntityFrameworkCore;
using MyProject.Domain.Interfaces;
using MyProject.Infrastructure.Context;
using MyProject.Infrastructure.Models;


namespace MyProject.Infrastructure.Repositories;


public class EmployeeRepository : GenericRepository<Employee>
{
    public EmployeeRepository(ApplicationDbContext context) : base(context) { }


    public async Task<Employee> GetByEmailAsync(string email)
    {
        return await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
    }
}