using FrameWorkRHP_Mono.Core.Models.Custom;
using FrameWorkRHP_Mono.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Xml.Linq;

namespace FrameWorkRHP_Mono.Infrastructure.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly AppsDbContext _context;
        //The following Variable is going to hold the DbSet Entity
        protected readonly DbSet<TEntity> _dbSet;
        public GenericRepository(AppsDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(object ParamId)
        {
            return await _dbSet.FindAsync(ParamId);
        }
         
        public async Task InsertAsync(TEntity Entity)
        {

            await _dbSet.AddAsync(Entity);
        }

        public async Task UpdateAsync(TEntity Entity)
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

        public async Task<IEnumerable<TEntity>> GetListByExpressionAsync(IEnumerable<Expression<Func<TEntity, bool>>> filter  = null, 
                                                                        Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderBy = null,  //Iquarble digunakan untuk parameter defisini atas order quarble bukan untuk di passing dari method pemanggil
                                                                        string includeProperties = "")
        {

            #region Sample to hit
            /*
                // Assuming you have a repository instance
               var repository = new YourRepository();

               // Example 1: Get all entities
               var allEntities = repository.Get();

               // Example 2: Get entities with a filter
               var filteredEntities = repository.Get(e => e.Age > 18);

               // Example 3: Get entities with filter and order by
               var filteredAndOrderedEntities = repository.Get(
                   filter: e => e.Age > 18,
                   orderBy: q => q.OrderBy(e => e.Name));

               // Example 4: Get entities with include properties
               var entitiesWithIncludedProperties = repository.Get(includeProperties: "Orders");

             */
            #endregion

            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                foreach (var additionalFilter in filter)
                {
                    query = query.Where(additionalFilter);
                }
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }

        }

        public async Task<TEntity> GetByExpressionAsync(IEnumerable<Expression<Func<TEntity, bool>>> filter = null, 
                                                    Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderBy = null, //Iquarble digunakan untuk parameter defisini atas order quarble bukan untuk di passing dari method pemanggil
                                                    string includeProperties = "")
        {

            #region Sample to hit
            /*
                // Assuming you have a repository instance
               var repository = new YourRepository();

               // Example 1: Get all entities
               var allEntities = repository.Get();

               // Example 2: Get entities with a filter
               var filteredEntities = repository.Get(e => e.Age > 18);

               // Example 3: Get entities with filter and order by
               var filteredAndOrderedEntities = repository.Get(
                   filter: e => e.Age > 18,
                   orderBy: q => q.OrderBy(e => e.Name));

               // Example 4: Get entities with include properties
               var entitiesWithIncludedProperties = repository.Get(includeProperties: "Orders");

             */
            #endregion

            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                foreach (var additionalFilter in filter)
                {
                    query = query.Where(additionalFilter);
                }
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).FirstOrDefault();
            }
            else
            {
                return query.FirstOrDefault();
            } 
        }
       

    }
}
