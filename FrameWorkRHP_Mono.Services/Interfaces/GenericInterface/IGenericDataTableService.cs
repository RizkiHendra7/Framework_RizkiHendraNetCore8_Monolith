using FrameWorkRHP_Mono.Core.Models.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWorkRHP_Mono.Services.Interfaces.GenericInterface
{
    public interface IGenericDataTableService
    {
        Task<cstmResultModelDataTable> getWithDataTable<ModelParam>(string ParamQuery, int ParamDraw);
    }
}
