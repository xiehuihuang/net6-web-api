using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.DTO;
using Project.Framework.Api;
using Project.Interfaces;
using System.Text.RegularExpressions;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly ILogger<RegisterController> _logger;
        private readonly IRegisterService _IRegisterService;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="iRegisterService"></param>
        public RegisterController(ILogger<RegisterController> logger, IRegisterService iRegisterService)
        {
            _logger = logger;
            _IRegisterService = iRegisterService;
        }

        #region 注册

        /// <summary>
        /// 发送短信验证码
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
            ApiResult ajaxResult = this._IRegisterService.CheckPhoneNumberBeforeSend(mobile);
            if (!ajaxResult.Success)//校验失败
            {
                return new JsonResult(ajaxResult);
            }
            else
            {
                var result = _IRegisterService.SendVerifyCode(mobile);
                return new JsonResult(result);
            }
        }


        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register(RegisterModel registerModel)
        {
            var result = _IRegisterService.Register(registerModel);
            return new JsonResult(result);
        }

        #endregion

    }
}
