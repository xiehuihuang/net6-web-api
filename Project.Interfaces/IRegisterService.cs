using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.DTO;
using Project.Framework.Api;

namespace Project.Interfaces
{
    public interface IRegisterService : IBaseService
    {
        /// <summary>
        /// 验证手机号
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public ApiResult CheckPhoneNumberBeforeSend(string mobile);

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public ApiResult SendVerifyCode(string phone);

        /// <summary>
        /// 前台注册用户
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        public ApiResult Register(RegisterModel registerModel);
    }
}
