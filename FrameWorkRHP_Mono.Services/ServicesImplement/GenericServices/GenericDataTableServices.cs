using FrameWorkRHP_Mono.Core.Models.Custom;
using FrameWorkRHP_Mono.Infrastructure.Repository;
using FrameWorkRHP_Mono.Infrastructure.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWorkRHP_Mono.Services.ServicesImplement.GenericServices
{
    public class GenericDataTableServices : IGenericDataTables
    {
        public IUnitOfWork _unitOfWork;
        public GenericDataTableServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
         
        public Task<cstmResultModelDataTable> getWithDataTable<ModelParam>(string ParamQuery, string ParamFilter, int ParamDraw)
        {
            return _unitOfWork.genericDataTables.getWithDataTable<ModelParam>(ParamQuery, ParamFilter, ParamDraw);
        }
    }
}
