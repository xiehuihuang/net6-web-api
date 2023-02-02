using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Project.WebApi.Jwt;
using SqlSugar;
using System.Reflection;
using Project.Framework.RedisHelper.Init;
using Project.Framework.RedisHelper.Service;
using Project.Framework.DatabaseConnectionOption;

namespace Project.WebApi.Register
{
    /// <summary>
    /// 替换Autofac
    /// 注册抽象和服务
    /// </summary>
    public static class AutofaceExtend
    {
        /// <summary>
        /// 扩展方法
        /// </summary>
        /// <param name="applicationBuilder"></param>
        public static void RegisterAutofac(this WebApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());//通过工厂替换，把Autofac整合进来
            applicationBuilder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
             {
                 #region 注册每个控制器和抽象之间的关系
                 {
                     var controllerBaseType = typeof(ControllerBase);
                     containerBuilder.RegisterAssemblyTypes(typeof(Program).Assembly)
                         .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType);
                 }
                 #endregion

                 #region 通过接口和实现类所在程序集注册 
                 {
                     Assembly interfaceAssembly = Assembly.Load("Project.Interfaces");
                     Assembly serviceAssembly = Assembly.Load("Project.Services");
                     containerBuilder.RegisterAssemblyTypes(interfaceAssembly, serviceAssembly).AsImplementedInterfaces();
                 }
                 #endregion

                 #region 注册SqlSugar 
                 {
                     containerBuilder.Register<ISqlSugarClient>(context =>
                     {
                         IConfiguration _Configuration = context.Resolve<IConfiguration>();
                         CustomConnectionConfig customConnectionConfig = new CustomConnectionConfig();
                         _Configuration.Bind("MasterSlaveConnectionStrings", customConnectionConfig);
                         ConnectionConfig connectionConfig = new ConnectionConfig()
                         {
                             ConnectionString = customConnectionConfig.ConnectionString,//主库
                                                                                        //DbType = DbType.SqlServer,
                             DbType = DbType.MySql,        //数据库类型 ： .SqlServer、.MySql
                             IsAutoCloseConnection = true, //开启自动释放模式和EF原理一样
                             SlaveConnectionConfigs = customConnectionConfig.SlaveConnectionConfigs.Select(c => new SlaveConnectionConfig() { ConnectionString = c.ConnectionString, HitRate = c.CustomHitRate }).ToList()
                         };
                         SqlSugarClient db = new SqlSugarClient(connectionConfig);
                         return db;
                         //SqlSugarClient client = new SqlSugarClient(new ConnectionConfig()
                         //{
                         //    ConnectionString = "Data Source=127.0.0.1;Initial Catalog=projectwebapi;Persist Security Info=True;User ID=sa;Password=123456",
                         //    DbType = DbType.SqlServer,           //数据库类型
                         //    InitKeyType = InitKeyType.Attribute, //从特性读取主键和自增列信息
                         //    SlaveConnectionConfigs = new List<SlaveConnectionConfig> {
                         //        new SlaveConnectionConfig(){
                         //             ConnectionString="Data Source=127.0.0.1;Initial Catalog=projectwebapi;Persist Security Info=True;User ID=sa;Password=123456",
                         //              HitRate=10
                         //        }
                         //}
                         //});
                         //client.Aop.OnLogExecuting = (sql, par) =>
                         //{
                         //    Console.WriteLine("\r\n");
                         //    Console.WriteLine($"Sql语句:{sql}");
                         //    Console.WriteLine($"=========================================================================================================================================================================================================");
                         //};
                         //return client;
                     });
                 }
                 #endregion

                 #region 注册Redis
                 {
                     containerBuilder.RegisterType<RedisHashService>();
                     containerBuilder.RegisterType<RedisListService>();
                     containerBuilder.RegisterType<RedisSetService>();
                     containerBuilder.RegisterType<RedisStringService>();
                     containerBuilder.RegisterType<RedisZSetService>();
                 }
                 #endregion

                 #region 注册生成Token
                 {
                     containerBuilder.RegisterType<HSJWTService>().As<IJWTService>();
                 }
                 #endregion
             });

            applicationBuilder.Services.Configure<RedisConfigInfo>(applicationBuilder.Configuration.GetSection("RedisConfigInfo"));
        }
    }
}
