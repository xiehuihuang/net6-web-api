using AutoMapper;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.DbModels;
using Project.DTO;
using Project.Framework;
using Project.Framework.Api;
using Project.Framework.RedisHelper.Service;
using Project.Interfaces;

namespace Project.Services
{
    /// <summary>
    /// 登录相关
    /// </summary>
    public class LoginService : BaseService, ILoginService
    {
        private static readonly string KEY_PREFIX = "userLogin:verify:code:";
        private readonly RedisStringService _RedisStringService;
        private readonly IMapper _IMapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="redisStringService"></param>
        public LoginService(ISqlSugarClient client, RedisStringService redisStringService, IMapper iMapper) : base(client)
        {
            _RedisStringService = redisStringService;
            _IMapper = iMapper;
        }

        /// <summary>
        /// 校验验证码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ApiResult CheckCode(string code, string mobile)
        {
            string key = KEY_PREFIX + mobile;
            string redeisCode = _RedisStringService.Get<string>(key);
            if (redeisCode != null && string.Equals(redeisCode, code, StringComparison.CurrentCultureIgnoreCase))
            {
                return new ApiResult();
            }
            else
            {
                return new ApiResult()
                {
                    Message = "验证码错误"
                };
            }

        }




        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public ApiResult SendVerifyCode(string phone)
        {
            var count = Query<User>(c => c.Mobile.Equals(phone.Trim())).Count();
            if (count <= 0)
            {
                return new ApiResult()
                {
                    Message = "手机号不存在"
                };
            }

            Random random = new Random();
            string code = random.Next(1000, 9999).ToString();// 生成随机6位数字验证码

            string key = KEY_PREFIX + phone;
            _RedisStringService.Set(key, code, TimeSpan.FromMinutes(5));// 把验证码存储到redis中  5分钟有效,有则覆盖
            _RedisStringService.Set(key + "1m1t", code, TimeSpan.FromMinutes(1));//一分钟只能发一次 
            var sendResult = SMSTool.SendValidateCode(phone, code);// 调用发送短信的方法
            ApiResult apiResult = new ApiResult()
            {
                Data = code
            };
            if (sendResult.Item1 == false)
            {
                apiResult.Message = sendResult.Item3;
            }
            return apiResult;
        }

        /// <summary>
        /// 通过手机号查询
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public UserDTO FindUserByMobile(string mobile)
        {
            User User = _Client.Queryable<User>().Where(c => c.Mobile.Equals(mobile)).First();
            return _IMapper.Map<User, UserDTO>(User);
        }

        /// <summary>
        /// 用户名和密码查询
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public UserDTO FindUserByNamePass(string userName, string passWord)
        {
            string encryptPass = MD5Encrypt.Encrypt(passWord);
            User User = _Client.Queryable<User>()
                .Where(c => c.Name.Equals(userName) && c.Password.Equals(encryptPass)).First();
            return _IMapper.Map<User, UserDTO>(User);
        }

        /// <summary>
        /// 通过手机号查询
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        UserInfoDTO ILoginService.FindUserByMobile(string mobile)
        {
            User user = _Client.Queryable<User>().Where(c => c.Mobile.Equals(mobile)).First();
            return _IMapper.Map<User, UserInfoDTO>(user);
        }

        /// <summary>
        /// 用户名和密码查询
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        UserInfoDTO ILoginService.FindUserByNamePass(string userName, string passWord)
        {
            string encryptPass = MD5Encrypt.Encrypt(passWord);
            User user = _Client.Queryable<User>()
                .Where(c => c.Name.Equals(userName) && c.Password.Equals(encryptPass)).First();
            return _IMapper.Map<User, UserInfoDTO>(user);
        }
    }
}
