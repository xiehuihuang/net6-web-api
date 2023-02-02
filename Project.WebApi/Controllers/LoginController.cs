using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.DTO;
using Project.Framework.Api;
using Project.Interfaces;
using Project.WebApi.Jwt;
using System.Text.RegularExpressions;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private IJWTService _jwtService;
        private ILoginService _loginService;
        public LoginController(ILogger<LoginController> logger, IJWTService iJWTService, ILoginService iLoginService) {
            _logger = logger;
            _jwtService = iJWTService;
            _loginService = iLoginService;
        }
        /// <summary>
        /// 手机号登录
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="smsVerificationCode"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult LoginByMobile(string mobile, string smsVerificationCode)
        {
            if (string.IsNullOrWhiteSpace(mobile))
            {
                return new JsonResult(new ApiResult()
                {
                    Message = "请输入手机号"
                });
            }
            if (new Regex("^1[3458][0-9]{9}$").IsMatch(mobile) == false)
            {
                return new JsonResult(new ApiResult()
                {
                    Message = "手机号不合法"
                });
            }

            if (string.IsNullOrWhiteSpace(smsVerificationCode))
            {
                return new JsonResult(new ApiResult() { Message = "请输入验证码" });
            }
            //ApiResult checkCodeResult = _LoginService.CheckCode(smsVerificationCode, mobile);
            //if (checkCodeResult.Success == false)
            //{
            //    return new JsonResult(checkCodeResult);
            //}
            UserInfoDTO user = _loginService.FindUserByMobile(mobile);
            if (user == null)
            {
                return new JsonResult(new ApiResult()
                {
                    Message = "手机号不存在"
                });
            }
            ApiResult result = this._jwtService.GetToken(user);
            result.Tag = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            return new JsonResult(result);
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult SendVerifyCode(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile))
            {
                return new JsonResult(new ApiResult()
                {
                    Message = "请输入手机号"
                });
            }
            if (new Regex("^1[3458][0-9]{9}$").IsMatch(mobile) == false)
            {
                return new JsonResult(new ApiResult()
                {
                    Message = "手机号不合法"
                });
            }
            var result = _loginService.SendVerifyCode(mobile);
            return new JsonResult(result);
        }

        /// <summary>
        /// 用户名密码登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult LoginByNamePass(string name, string password)
        {
            UserInfoDTO user = _loginService.FindUserByNamePass(name, password);
            ApiResult result = this._jwtService.GetToken(user);
            result.Tag = user;
            this._logger.LogInformation($"{this.GetType().Name} 登录成功");
            return new JsonResult(result);
        }

    }
}
