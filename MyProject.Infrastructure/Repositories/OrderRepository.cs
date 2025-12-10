
using MyProject.Domain.Interfaces;
using MyProject.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using MyProject.Infrastructure.Models;


namespace MyProject.Infrastructure.Repositories;


public class OrderRepository : GenericRepository<Order>
{
    public OrderRepository(ApplicationDbContext context) : base(context) { }
}