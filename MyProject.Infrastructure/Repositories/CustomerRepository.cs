using MyProject.Infrastructure.Context;
using MyProject.Infrastructure.Models;


namespace MyProject.Infrastructure.Repositories;


public class CustomerRepository : GenericRepository<Customer>
{
    public CustomerRepository(ApplicationDbContext context) : base(context) { }
}