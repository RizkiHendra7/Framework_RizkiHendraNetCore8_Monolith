using FrameWorkRHP_Mono.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FrameWorkRHP_Mono.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppsDbContext _context;
        //The following Variable is going to hold the DbSet Entity
        protected readonly DbSet<T> _dbSet;
        public GenericRepository(AppsDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(object ParamId)
        {
            return await _dbSet.FindAsync(ParamId);
        }

        public async Task InsertAsync(T Entity)
        {

            await _dbSet.AddAsync(Entity);
        }

        public async Task UpdateAsync(T Entity)
        {

            _dbSet.Update(Entity);
        }

        public async Task DeleteAsync(object ParamId)
        {

            var entity = await _dbSet.FindAsync(ParamId);
            if (entity != null)
            {

                _dbSet.Remove(entity);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
