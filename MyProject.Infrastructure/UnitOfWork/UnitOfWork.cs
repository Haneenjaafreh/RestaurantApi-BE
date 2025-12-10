
using MyProject.Infrastructure.Context;
using MyProject.Domain.Interfaces;
using MyProject.Infrastructure.Repositories;
using MyProject.Infrastructure.Models;

namespace MyProject.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGenericRepository<Employee> Employees { get; }
        public IGenericRepository<Menu> Menus { get; }
        public IGenericRepository<Order> Orders { get; }
        public IGenericRepository<OrderItem> OrderItems { get; }
        public IGenericRepository<RestaurantTable> Tables { get; }
        public IGenericRepository<Customer> Customers { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Employees = new GenericRepository<Employee>(context);
            Menus = new GenericRepository<Menu>(context);
            Orders = new GenericRepository<Order>(context);
            OrderItems = new GenericRepository<OrderItem>(context);
            Tables = new GenericRepository<RestaurantTable>(context);
            Customers = new GenericRepository<Customer>(context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
