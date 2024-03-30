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
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

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
            #region jika menggunakan session
            //_httpContextAccessor.HttpContext.Items[paramKey] = paramData;
            #endregion

            #region jika menggunakan Identity
            var user = _httpContextAccessor.HttpContext.User;
            var currentClaims = user.Claims.ToList();

            // Find and update the desired claim
            var claimToUpdate = currentClaims.FirstOrDefault(c => c.Type == paramKey);
            if (claimToUpdate != null)
            {
                var updatedClaim = new Claim(paramKey, paramData);
                currentClaims.Remove(claimToUpdate);
                currentClaims.Add(updatedClaim);
            }

            // Re-issue authentication token with updated claims
            var claimsIdentity = new ClaimsIdentity(currentClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            #endregion 
        }
        public async Task<TEntity> getSession<TEntity>(string paramKey) where TEntity : class
        {
            string ObjData = _httpContextAccessor.HttpContext.Session.GetString(paramKey);
           return JsonConvert.DeserializeObject<TEntity>(ObjData);
            
        }         
        public async void SignInAsync(string paramToken, cstmSessionModel paramUserDt)
        {
             
            // Create claims
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,paramUserDt.userDt.Txtfullname),
                        new Claim(clsGlobalConstant.SessionNameKey,paramToken)
                        // Add more claims if needed
                    };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Create authentication properties
            var authProperties = new AuthenticationProperties
            {
                // If set to true, the sign-in cookie will persist across browser sessions. In other words, the user remains signed in even after closing and reopening the browser.
                // If set to false, the cookie is session-based and will expire when the browser is closed.
                IsPersistent = true
            };
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), authProperties);


            #region jika menggunakan session
            //this.setSession(clsGlobalConstant.SessionNameKey, paramToken);
            #endregion
        }
        public async void SignOutAsync()
        {
            #region jika menggunakan session
            //_httpContextAccessor.HttpContext.Session.Remove(clsGlobalConstant.SessionNameKey);
            #endregion
            await _httpContextAccessor.HttpContext.SignOutAsync();

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
                    var token = _staticHttpContextAccessor.HttpContext.User.FindFirst(clsGlobalConstant.SessionNameKey)?.Value;
                    //var token = _staticHttpContextAccessor.HttpContext.Session.GetString(clsGlobalConstant.SessionNameKey); // jika pakai session
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
                    return JsonConvert.DeserializeObject<cstmSessionModel>((jwtToken.Claims.First(x => x.Type == clsGlobalConstant.SessionNameKey).Value));
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
