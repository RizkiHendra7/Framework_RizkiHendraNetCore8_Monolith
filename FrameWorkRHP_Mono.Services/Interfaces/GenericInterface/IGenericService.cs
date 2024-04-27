﻿
using FrameWorkRHP_Mono.Core.Models.Custom;

namespace FrameWorkRHP_Mono.Services.Interfaces.GenericInterface
{
    public interface IGenericService<T> where T : class
    {
        Task<bool> CreateData(T ParamModels);
        Task<IEnumerable<T>> GetAllData();
        Task<IEnumerable<T>> GetAllActiveData();
        Task<T> GetDataById(int ParamIntId);
        Task<bool> UpdateData(T ParamModels);
        Task<bool> DeleteData(int ParamIntId);
        Task<bool> Validation(T ParamModels);
        Task<cstmResultModelDataTable> getWithDataTable(cstmFilterDataTable paramModel);
    }
}
