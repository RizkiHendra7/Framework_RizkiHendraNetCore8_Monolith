using FrameWorkRHP_Mono.Core.Models.Custom;
using FrameWorkRHP_Mono.Core.Models.EF;
using FrameWorkRHP_Mono.Infrastructure.UOW;
using FrameWorkRHP_Mono.Services.Interfaces.GenericInterface; 

namespace FrameWorkRHP_Mono.Services.ServicesImplement
{
    public class MUserService : IGenericService<Muser> 
    {
        public IUnitOfWork _unitOfWork;

        public MUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateData(Muser ParamModels)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                await _unitOfWork.MUsers.InsertAsync(ParamModels);
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
                await _unitOfWork.MUsers.DeleteAsync(ParamIntId);
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

        public Task<IEnumerable<Muser>> GetAllActiveData()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Muser>> GetAllData()
        {
            return await _unitOfWork.MUsers.GetAllAsync();
        }

        public async Task<Muser> GetDataById(int ParamIntId)
        {
            var MUserData = await _unitOfWork.MUsers.GetByIdAsync(Convert.ToInt32(ParamIntId));
            MUserData = MUserData == null ? new Muser() : MUserData;
            return MUserData;
        }

        public async Task<cstmResultModelDataTable> getDataPaging(cstmFilterDataTable paramModel)
        {
            var query = "SELECT\r\n   *, COUNT(*) OVER () AS TOTALDATA\r\nFROM\r\n  muser\r\nORDER BY\r\n   intuserid \r\nOFFSET 2 \r\nLIMIT 1;";
            var result = await _unitOfWork.MUsers.getWithDataTable(query,paramModel.draw); 
            return result;
        }

        public async Task<bool> UpdateData(Muser ParamModels)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                await _unitOfWork.MUsers.UpdateAsync(ParamModels);
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
    }
}
