using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Project.DTO;
using Project.Framework.Api;

namespace Project.WebApi.Jwt
{
    /// <summary>
    /// 非对称可逆加密
    /// </summary>
    public class RSSJWTervice : IJWTService

    {
        #region Option注入
        private readonly JWTTokenOptions _JWTTokenOptions;
        public RSSJWTervice(IOptionsMonitor<JWTTokenOptions> jwtTokenOptions)
        {
            _JWTTokenOptions = jwtTokenOptions.CurrentValue;
        }
        #endregion

        public ApiResult GetToken(UserInfoDTO user)
        {
            #region 使用加密解密Key  非对称 
            string keyDir = Directory.GetCurrentDirectory();
            if (RSAHelper.TryGetKeyParameters(keyDir, true, out RSAParameters keyParams) == false)
            {
                keyParams = RSAHelper.GenerateAndSaveKey(keyDir);
            }
            #endregion
            Claim[] claims = new[]
            {
               new Claim(ClaimTypes.Name, user.Name),
               new Claim(ClaimTypes.MobilePhone, user.Mobile)
            };

            SigningCredentials credentials = new SigningCredentials(new RsaSecurityKey(keyParams), SecurityAlgorithms.RsaSha256Signature);

            var token = new JwtSecurityToken(
               issuer: _JWTTokenOptions.Issuer,
               audience: _JWTTokenOptions.Audience,
               claims: claims,
               expires: DateTime.Now.AddMinutes(60),//5分钟有效期
               signingCredentials: credentials);

            var handler = new JwtSecurityTokenHandler();
            string tokenString = handler.WriteToken(token);
            return new ApiResult()
            {
                Data = tokenString
            };
        }
    }
}
