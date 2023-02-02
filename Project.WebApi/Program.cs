using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Project.DTO;
using Project.Interfaces;
using Project.WebApi.Register;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(typeof(AutoMapObject)); //֧��Automapperӳ��
//builder.Services.AddControllers();
//����Json�������ڸ�ʽ
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// ע��log4net��־�ļ�
#region log4net
{
    //Nuget���룺logenet��Microsoft.Extensions.Loggins.Log4Net.AspNetCore
    builder.Logging.AddLog4Net(@"Config/log4net.config");
}
#endregion
#region NLogin
{
    //Nuget���룺NLog.Web.AspNetCore
    //builder.Logging.AddNLog(@"Config/NLog.config");
}
#endregion



builder.RegisterSwagger();    // Swagger���÷�װ��ע��
builder.RegisterAutofac();    //ҵ���߼���Services�����ע�� 
builder.RegisterCors("CORS"); //��ӿ�����Ե�ע��

// ע��JWT
builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection("JWTTokenOptions"));

//֧����Ȩ��
builder.AuthorizationExt();

var app = builder.Build();
app.UseCors("CORS");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<StreamReadMiddleware>(); // ��ȡͼƬ��Ϣ

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
