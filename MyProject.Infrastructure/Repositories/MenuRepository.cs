
using MyProject.Domain.Interfaces;
using MyProject.Infrastructure.Context;
using MyProject.Infrastructure.Models;


namespace MyProject.Infrastructure.Repositories;


public class MenuRepository : GenericRepository<Menu>
{
    public MenuRepository(ApplicationDbContext context) : base(context) { }
}