using FrameWorkRHP_Mono.Core.Models.Custom;
using FrameWorkRHP_Mono.Core.Models.EF;
using FrameWorkRHP_Mono.Infrastructure.UOW;
using FrameWorkRHP_Mono.Services.Interfaces.GenericInterface; 

namespace FrameWorkRHP_Mono.Services.ServicesImplement
{
    public class MRoleService : IGenericService<Mrole>
    {
        public IUnitOfWork _unitOfWork;

        public MRoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateData(Mrole ParamModels)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                await _unitOfWork.MRoles.InsertAsync(ParamModels);
                await _unitOfWork.Save();
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<bool> DeleteData(int ParamIntId)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                await _unitOfWork.MRoles.DeleteAsync(ParamIntId);
                await _unitOfWork.Save();
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public Task<IEnumerable<Mrole>> GetAllActiveData()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Mrole>> GetAllData()
        {
            return await _unitOfWork.MRoles.GetAllAsync();
        }

        public async Task<Mrole> GetDataById(int ParamIntId)
        {
            var MRoleData = await _unitOfWork.MRoles.GetByIdAsync(Convert.ToInt32(ParamIntId));
            MRoleData = MRoleData == null ? new Mrole() : MRoleData;
            return MRoleData;
        }

        public async Task<bool> UpdateData(Mrole ParamModels)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                await _unitOfWork.MRoles.UpdateAsync(ParamModels);
                await _unitOfWork.Save();
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<cstmResultModelDataTable> getDataPaging(cstmFilterDataTable paramModel)
        {
            var query = "SELECT\r\n   *, COUNT(*) OVER () AS TOTALDATA\r\nFROM\r\n  mrole m \r\nORDER BY\r\n   introleid \r\nOFFSET 2 \r\nLIMIT 1;";
            var result = await _unitOfWork.MUsers.getWithDataTable(query, paramModel.draw);
            return result;
        }

    }
}
