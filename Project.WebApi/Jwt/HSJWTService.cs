using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Project.DTO;
using Project.Framework.Api;

namespace Project.WebApi.Jwt
{
    /// <summary>
    /// 对称可逆加密
    /// </summary>
    public class HSJWTService : IJWTService
    {
        private readonly JWTTokenOptions _JWTTokenOptions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jwtTokenOptions"></param>
        public HSJWTService(IOptionsMonitor<JWTTokenOptions> jwtTokenOptions)
        {
            _JWTTokenOptions = jwtTokenOptions.CurrentValue;
        }


        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ApiResult GetToken(UserInfoDTO user)
        {
            var claims = new[]
            {
               new Claim("UserId", user.Id.ToString()),
               new Claim(ClaimTypes.Name, user.Name),
               new Claim(ClaimTypes.MobilePhone, user.Mobile)
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWTTokenOptions.SecurityKey!));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
             issuer: _JWTTokenOptions.Issuer,
             audience: _JWTTokenOptions.Audience,
             claims: claims,
             expires: DateTime.Now.AddMinutes(5),//5分钟有效期
             signingCredentials: creds);
            string returnToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new ApiResult()
            {
                Data = returnToken
            };
        }
    }
}
