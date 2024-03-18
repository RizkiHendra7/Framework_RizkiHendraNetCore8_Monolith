using FrameWorkRHP_Mono.Core.Models.Custom;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using FrameWorkRHP_Mono.Services.Interfaces.GenericInterface;
using FrameWorkRHP_Mono.Core.CommonFunction;

namespace FrameWorkRHP_Mono.Services.ServicesImplement.GenericServices
{
    public class SessionService : ISessionService
    { 
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static IHttpContextAccessor _staticHttpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _staticHttpContextAccessor = httpContextAccessor;

        } 
        public async void setSession(string paramKey,string paramData)
        {
            _httpContextAccessor.HttpContext.Session.SetString(paramKey, paramData);
        }

        public async void changeSession(string paramKey, string paramData)
        { 
            _httpContextAccessor.HttpContext.Items[paramKey] = paramData;
        }
        public async Task<TEntity> getSession<TEntity>(string paramKey) where TEntity : class
        {
            string ObjData = _httpContextAccessor.HttpContext.Session.GetString(paramKey);
           return JsonConvert.DeserializeObject<TEntity>(ObjData);
            
        }

        #region User Session Login   
        public static cstmSessionModel dtLogin
        {
            get
            {
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(clsGlobalConstant.SessionSecretKey);
                    var token = _staticHttpContextAccessor.HttpContext.Session.GetString(clsGlobalConstant.SessionKey);
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken; 
                    return JsonConvert.DeserializeObject<cstmSessionModel>((jwtToken.Claims.First(x => x.Type == clsGlobalConstant.SessionKey).Value));
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("There Something wrong while validation token => " + ex.Message);
                };
            }
        }
        #endregion

    }
}
