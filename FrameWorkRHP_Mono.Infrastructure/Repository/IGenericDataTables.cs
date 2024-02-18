using FrameWorkRHP_Mono.Core.Models.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWorkRHP_Mono.Infrastructure.Repository
{
    public interface IGenericDataTables 
    {
          Task<cstmResultModelDataTable> getWithDataTable<ModelParam>(string ParamQuery, string ParamFilter, int ParamDraw);
    }
}
