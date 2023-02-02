using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.DTO;
using Project.Framework.Api;
using Project.Interfaces;
using Project.Services;
using Project.WebApi.Jwt;
using System.Text.RegularExpressions;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private IUserService _userService;
        public UserController(ILogger<UserController> logger, IUserService iuserService)
        {
            _logger = logger;
            _userService = iuserService;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public UserInfoDTO GetCurrentUserInfo(int userid)
        {
            UserInfoDTO userInfo = _userService.GetCurrentUserInfo(userid);
            return userInfo;
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public JsonResult UpUserInfo(UserDTO user)
        {
            var userId = HttpContext.User.Claims.First(c => c.Type.Equals("UserId")).Value;
            return new JsonResult(_userService.UpUserInfo(user));
        }
    }
}

