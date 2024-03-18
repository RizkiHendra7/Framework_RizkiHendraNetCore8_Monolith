using FrameWorkRHP_Mono.Core.CommonFunction;
using FrameWorkRHP_Mono.Core.Models.Custom;
using FrameWorkRHP_Mono.Core.Models.EF;
using FrameWorkRHP_Mono.Infrastructure.UOW;
using FrameWorkRHP_Mono.Services.Interfaces;
using FrameWorkRHP_Mono.Services.Interfaces.GenericInterface;
using FrameWorkRHP_Mono.Services.ServicesImplement.GenericServices;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FrameWorkRHP_Mono.Services.ServicesImplement
{
    public class LoginServices : ILogin
    {
        public IUnitOfWork _unitOfWork;
        public ISessionService _httpContextServices;
        public LoginServices(IUnitOfWork unitOfWork,ISessionService httpContextServices)
        {
            _unitOfWork = unitOfWork;
            _httpContextServices = httpContextServices;
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

                   string token =  await new clsGlobalJWT().GeneratedJWTToken(clsGlobalConstant.SessionKey, dtSession);
                   string jsonMuser = JsonConvert.SerializeObject(MUserData);
                   _httpContextServices.setSession(clsGlobalConstant.SessionKey, token);
                }

                var dicobamauEggPakeStatic = SessionService.dtLogin;

                return result;
            }
            catch (Exception ex) {
                throw;
            } 
        }
    }
}
