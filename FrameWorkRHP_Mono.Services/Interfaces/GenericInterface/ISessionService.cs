using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWorkRHP_Mono.Services.Interfaces.GenericInterface
{
    public interface ISessionService
    {
          void setSession(string paramKey, string paramData);
          void changeSession(string paramKey, string paramData);
          Task<TEntity> getSession<TEntity>(string paramKey) where TEntity : class;
    }
}
