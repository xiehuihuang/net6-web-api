using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Project.WebApi.Register
{

    /// <summary>
    /// 读取图片
    /// </summary>
    public class StreamReadMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<StreamReadMiddleware> _logger;
        private IConfiguration _IConfiguration;

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="next"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public StreamReadMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<StreamReadMiddleware> logger)
        {
            _next = next;
            _IConfiguration = configuration;
            _logger = logger;
        }


        /// <summary>
        /// 执行中间件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task InvokeAsync(HttpContext context)
        {
            string? stringPath = context.Request.Path.Value;
            if (!string.IsNullOrWhiteSpace(stringPath) && (stringPath.Contains("jpg") || stringPath.Contains("png")))
            {
                #region MyRegion
                {
                    context.Request.EnableBuffering();
                    var reader = new StreamReader(context.Request.Body);
                    var content = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;
                }
                {
                    var responseStream = context.Response.Body;
                    var basefilePath = _IConfiguration["UploadFilePath"];
                    if (string.IsNullOrWhiteSpace(basefilePath))
                    {
                        throw new Exception("请配置文件上传的保存地址");
                    }
                    using (FileStream newStream = new FileStream($"{basefilePath}/file/{context.Request.Path.Value}", FileMode.Open))
                    {
                        context.Response.Body = newStream;
                        newStream.Position = 0;
                        var responseReader = new StreamReader(newStream);
                        var responseContent = await responseReader.ReadToEndAsync();
                        newStream.Position = 0;
                        await newStream.CopyToAsync(responseStream);
                        context.Response.Body = responseStream;
                    }
                }
                #endregion
            }
            else
            {
                await _next(context);
            }
        }
    }
}
