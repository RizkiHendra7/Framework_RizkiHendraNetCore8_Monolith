using FrameWorkRHP_Mono.Core.CommonFunction;
using FrameWorkRHP_Mono.Core.Models.Custom;
using FrameWorkRHP_Mono.Core.Models.EF;
using FrameWorkRHP_Mono.Infrastructure.UOW;
using FrameWorkRHP_Mono.Services.Interfaces;
using FrameWorkRHP_Mono.Services.Interfaces.GenericInterface;
using FrameWorkRHP_Mono.Services.ServicesImplement.GenericServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Security.Claims; 

namespace FrameWorkRHP_Mono.Services.ServicesImplement
{
    public class LoginServices : ILogin
    {
        public IUnitOfWork _unitOfWork;
        public ISessionService _sessionService;
        public IHttpContextAccessor _httpContextAccessor;
        public LoginServices(IUnitOfWork unitOfWork,ISessionService sessionService, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _sessionService = sessionService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> LoginAsync(Muser paramData)
        {
            try
            {
                var result = true; 
                var filters = new List<Expression<Func<Muser, bool>>>();
                filters.Add(x => x.Txtusername.ToUpper().Equals(paramData.Txtusername.ToUpper()));
                filters.Add(x => x.Txtpassword.ToUpper().Equals(paramData.Txtpassword.ToUpper()));
                filters.Add(x => x.Bitactive == true); 

                var MUserData = await _unitOfWork.MUsers.GetByExpressionAsync(filters);
                result = MUserData == null ? false : true;

                if (result)
                {
                    //get role
                    var ltRole = await _unitOfWork.MRoles.GetAllAsync();

                    cstmSessionModel dtSession = new cstmSessionModel();
                    dtSession.userDt = MUserData;
                    dtSession.roleLt = ltRole;

                   string token =  await new clsGlobalJWT().GeneratedJWTToken(clsGlobalConstant.SessionNameKey, dtSession);
                   string jsonMuser = JsonConvert.SerializeObject(MUserData);
                    #region "JIKA MENGGUNAKAN SESSION"
                    _sessionService.SignInAsync(token, dtSession);
                    #endregion 
                }


                return result;
            }
            catch (Exception ex) {
                throw;
            } 
        }
    }
}
