using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWorkRHP_Mono.Infrastructure.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(object ParamId);
        Task InsertAsync(T ParamModel);
        Task UpdateAsync(T ParamModel);
        Task DeleteAsync(object ParamId);
        Task SaveAsync();
    }
}
