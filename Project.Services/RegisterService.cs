using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.DbModels;
using Project.Framework;
using Project.Framework.Api;
using Project.Framework.CustomEnum;
using Project.Framework.RedisHelper.Service;
using Project.Interfaces;
using Project.DTO;

namespace Project.Services
{
    /// <summary>
    /// 注册相关
    /// </summary>
    public class RegisterService : BaseService, IRegisterService
    {
        private static readonly string KEY_PREFIX = "user:verify:code:";

        private readonly RedisStringService _RedisStringService;
        public RegisterService(ISqlSugarClient client, RedisStringService redisStringService) : base(client)
        {
            _RedisStringService = redisStringService;
        }

        /// <summary>
        /// 验证手机号
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public ApiResult CheckPhoneNumberBeforeSend(string mobile)
        {
            ApiResult apiResult = new ApiResult();
            string key = KEY_PREFIX + mobile;
            if (!string.IsNullOrWhiteSpace(_RedisStringService.Get(key + "1m1t")))
            {
                apiResult.Message = "1分钟内只能发一次验证码";
                return apiResult;
            }
            var count = Query<User>(c => c.Mobile.Equals(mobile.Trim())).Count();
            if (count > 0)
            {
                apiResult.Message = "手机号码重复";
                return apiResult;
            }
            return apiResult;
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public ApiResult SendVerifyCode(string phone)
        {
            Random random = new Random();
            string code = random.Next(100000, 999999).ToString();// 生成随机6位数字验证码

            string key = KEY_PREFIX + phone;
            _RedisStringService.Set(key, code, TimeSpan.FromMinutes(5));// 把验证码存储到redis中  5分钟有效,有则覆盖
            _RedisStringService.Set(key + "1m1t", code, TimeSpan.FromMinutes(1));//一分钟只能发一次 

            ///调用接口 发送短信  付钱
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
        /// 用户注册
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        public ApiResult Register(RegisterModel registerModel)
        {
            string key = KEY_PREFIX + registerModel.Mobile;
            var smsVerificationCode = _RedisStringService.Get(key);
            if (string.IsNullOrWhiteSpace(smsVerificationCode) || string.Equals(smsVerificationCode.Replace("\"", ""), registerModel.SmsVerificationCode, StringComparison.CurrentCultureIgnoreCase) == false)
            {
                return new ApiResult()
                {
                    Message = "验证码错误"
                };
            }

            User user = new User();
            user.Name = registerModel.Name;
            user.Password = MD5Encrypt.Encrypt(registerModel.Password);
            user.Mobile = registerModel.Mobile;
            user.CreateTime = DateTime.Now;
            user.UserType = (int)UserTypeEnum.Member;
            user.Status = 1;  // 用户状态：0为冻结、1为正常、2为已删除
            var result = _Client.Insertable<User>(user).ExecuteCommand();
            return new ApiResult();
        }
    }
}
