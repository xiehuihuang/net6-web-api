using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.DTO;
using Project.Framework.Api;

namespace Project.Interfaces
{
    public interface ILoginService : IBaseService
    {
        /// <summary>
        /// 校验验证码是否正确
        /// </summary>
        /// <param name="code"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public ApiResult CheckCode(string code, string mobile);

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public ApiResult SendVerifyCode(string phone);

        /// <summary>
        /// 根据手机号查询
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public UserInfoDTO FindUserByMobile(string mobile);

        /// <summary>
        /// 根据用户名和密码查询用户
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public UserInfoDTO FindUserByNamePass(string userName, string passWord);
    }
}
