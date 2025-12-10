
using MyProject.Domain.Interfaces;
using MyProject.Infrastructure.Models;

namespace MyProject.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<Employee> Employees { get; }
        IGenericRepository<Menu> Menus { get; }
        IGenericRepository<Order> Orders { get; }
        IGenericRepository<OrderItem> OrderItems { get; }
        IGenericRepository<RestaurantTable> Tables { get; }
        IGenericRepository<Customer> Customers { get; }

        Task<int> SaveAsync();
    }
}
