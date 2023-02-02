using AutoMapper;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.DbModels;
using Project.DTO;
using Project.Framework.Api;
using Project.Interfaces;

namespace Project.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IMapper _IMapper;
        public UserService(ISqlSugarClient client, IMapper iMapper) : base(client)
        {
            _IMapper = iMapper;
        }

        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public UserInfoDTO GetCurrentUserInfo(int userid)
        {
            User user = _Client.Queryable<User>().First(c => c.Id == userid);
            user.Password = null;
            UserInfoDTO userInfoDTO = _IMapper.Map<User, UserInfoDTO>(user);
            return userInfoDTO;
        }


        /// <summary>
        ///  修改用户信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ApiResult UpUserInfo(UserDTO userInfo)
        {
            User user = _Client.Queryable<User>().First(c => c.Id == userInfo.Id);
            user.Name = userInfo.Name;
            user.Gender = userInfo.Gender;
            user.Birthday = userInfo.Birthday;
            if (_Client.Updateable<User>(user).ExecuteCommand() > 0)
            {
                return new ApiResult();
            }
            return new ApiResult() { Message = "修改失败" };
        }
    }
}
