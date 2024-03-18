using FrameWorkRHP_Mono.Core.Models.Custom;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FrameWorkRHP_Mono.Core.CommonFunction
{
    public class clsGlobalJWT
    { 
        public async Task<string> GeneratedJWTToken(string paramKeyClaim, object parmaData)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(clsGlobalConstant.SessionSecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                                new Claim(paramKeyClaim,JsonConvert.SerializeObject(parmaData))}),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<TEntity> ExtractJWTToken<TEntity>(string paramKeyClaim, string paramToken) where TEntity : class 
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(clsGlobalConstant.SessionSecretKey); 
                tokenHandler.ValidateToken(paramToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                return JsonConvert.DeserializeObject<TEntity>((jwtToken.Claims.First(x => x.Type == paramKeyClaim).Value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException("There Something wrong while validation token => " + ex.Message);
            };
        }

    }
}
