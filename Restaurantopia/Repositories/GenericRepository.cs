using Microsoft.EntityFrameworkCore;
using Restaurantopia.InterFaces;
using Restaurantopia.Models;

namespace Restaurantopia.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private MyDbContext _db;
        private DbSet<T> _dbSet;
        public GenericRepository(MyDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var item = await GetByIdAsync(id);
            _dbSet.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IQueryable<T> query = _dbSet;

            return await query.ToListAsync();
        }



        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
