using Microsoft.EntityFrameworkCore;
using MyProject.Application.Interfaces;
using MyProject.Infrastructure.Context;
using System.Linq.Expressions;

namespace MyProject.Infrastructure.Repositories
{
    public class Repository<T> : IRepository1<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(object id) => await _dbSet.FindAsync(id);
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task AddAsync(T entity) { await _dbSet.AddAsync(entity); await _context.SaveChangesAsync(); }
        public async Task Update(T entity) { _dbSet.Update(entity); await _context.SaveChangesAsync(); }
        public async Task Delete(T entity) { _dbSet.Remove(entity); await _context.SaveChangesAsync(); }
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) => await _dbSet.Where(predicate).ToListAsync();
    }
}
