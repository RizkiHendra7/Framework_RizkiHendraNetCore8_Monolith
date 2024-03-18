using FrameWorkRHP_Mono.Core.Models.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FrameWorkRHP_Mono.Infrastructure.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetListByExpressionAsync(
          IEnumerable<Expression<Func<TEntity, bool>>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          string includeProperties = "");
        Task<TEntity?> GetByIdAsync(object ParamId);
        Task<TEntity> GetByExpressionAsync(
          IEnumerable<Expression<Func<TEntity, bool>>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          string includeProperties = ""); 
        Task InsertAsync(TEntity ParamModel);
        Task UpdateAsync(TEntity ParamModel);
        Task DeleteAsync(object ParamId);
        Task SaveAsync();
    }
}
