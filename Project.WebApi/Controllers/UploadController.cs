using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Project.DTO;
using Project.Framework.Api;
using Project.Interfaces;

namespace Project.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UploadController : ControllerBase
    {
        private IConfiguration _IConfiguration;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public UploadController(IConfiguration configuration)
        {
            _IConfiguration = configuration;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult UpLoadImage([FromForm] IFormCollection formCollection)
        {
            FormFileCollection fileCollection = (FormFileCollection)formCollection.Files;

            List<string> fileList = new List<string>();
            foreach (IFormFile file in fileCollection)
            {
                StreamReader reader = new StreamReader(file.OpenReadStream());
                string content = reader.ReadToEnd();


                var uploadFilePath = _IConfiguration["UploadFilePath"];

                if (string.IsNullOrWhiteSpace(uploadFilePath))
                {
                    throw new Exception("请配置文件上传的保存地址");
                }
                string folderName = HttpContext.Request.Form["folderName"];
                if (string.IsNullOrWhiteSpace(folderName))
                {
                    folderName = "file";
                }

                uploadFilePath = $"{uploadFilePath}/file/{folderName}";

                if (Directory.Exists(uploadFilePath) == false)
                {
                    Directory.CreateDirectory(uploadFilePath);
                }
                string suffixName = file.FileName.Substring(file.FileName.LastIndexOf("."));
                string newFileName = $"{Guid.NewGuid().ToString()}{suffixName}";

                fileList.Add(Path.Combine(folderName, newFileName));
                string filename = Path.Combine(uploadFilePath, newFileName);
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }
            return new ApiResult()
            {
                Data = fileList
            };
        }

    }
}