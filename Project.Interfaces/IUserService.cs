using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.DbModels;
using Project.DTO;
using Project.Framework.Api;

namespace Project.Interfaces
{
    public interface IUserService : IBaseService
    {
        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public UserInfoDTO GetCurrentUserInfo(int userid);

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public ApiResult UpUserInfo(UserDTO userInfo);
    }
}
