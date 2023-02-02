using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Project.DTO;
using Project.Interfaces;
using Project.WebApi.Register;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(typeof(AutoMapObject)); //支持Automapper映射
//builder.Services.AddControllers();
//设置Json返回日期格式
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// 注册log4net日志文件
#region log4net
{
    //Nuget引入：logenet、Microsoft.Extensions.Loggins.Log4Net.AspNetCore
    builder.Logging.AddLog4Net(@"Config/log4net.config");
}
#endregion
#region NLogin
{
    //Nuget引入：NLog.Web.AspNetCore
    //builder.Logging.AddNLog(@"Config/NLog.config");
}
#endregion



builder.RegisterSwagger();    // Swagger配置封装的注册
builder.RegisterAutofac();    //业务逻辑层Services服务的注册 
builder.RegisterCors("CORS"); //添加跨域策略的注册

// 注册JWT
builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection("JWTTokenOptions"));

//支持授权的
builder.AuthorizationExt();

var app = builder.Build();
app.UseCors("CORS");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<StreamReadMiddleware>(); // 读取图片信息

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
