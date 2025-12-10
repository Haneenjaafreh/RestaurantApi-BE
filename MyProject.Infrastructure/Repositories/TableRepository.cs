using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyProject.Domain.Interfaces;
using MyProject.Infrastructure.Context;


namespace MyProject.Infrastructure.Repositories;


public class TableRepository : GenericRepository<Table>
{
    public TableRepository(ApplicationDbContext context) : base(context) { }
}