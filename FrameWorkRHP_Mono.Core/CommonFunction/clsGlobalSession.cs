using FrameWorkRHP_Mono.Core.Models.Custom;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace FrameWorkRHP_Mono.Core.CommonFunction
{
    public class clsGlobalSession  
    { 
        private readonly IHttpContextAccessor _httpContextAccessor;

        public clsGlobalSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

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
        private static IHttpContextAccessor _staticHttpContextAccessor;
        public static IHttpContextAccessor HttpContextAccessor
        {
            get { return _staticHttpContextAccessor; }
            set { _staticHttpContextAccessor = value; }
        }
        //public static cstmSessionModel dtLogin
        //{
        //    get
        //    {
        //        try
        //        {
        //            var tokenHandler = new JwtSecurityTokenHandler();
        //            var key = Encoding.ASCII.GetBytes(clsGlobalConstant.SessionSecretKey);
        //            var token = _staticHttpContextAccessor.HttpContext.Session.GetString(clsGlobalConstant.SessionNameKey);
        //            tokenHandler.ValidateToken(token, new TokenValidationParameters
        //            {
        //                ValidateIssuerSigningKey = true,
        //                IssuerSigningKey = new SymmetricSecurityKey(key),
        //                ValidateIssuer = false,
        //                ValidateAudience = false,
        //                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
        //                ClockSkew = TimeSpan.Zero
        //            }, out SecurityToken validatedToken);

        //            var jwtToken = (JwtSecurityToken)validatedToken;
        //            return JsonConvert.DeserializeObject<cstmSessionModel>((jwtToken.Claims.First(x => x.Type == clsGlobalConstant.SessionNameKey).Value));
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new ArgumentException("There Something wrong while validation token => " + ex.Message);
        //        };
        //    }
        //}
        #endregion

    }
}
