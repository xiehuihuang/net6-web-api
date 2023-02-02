// See https://aka.ms/new-console-template for more information
using SqlSugar;

Console.WriteLine("------SqlSugar生成实体对象------");
try
{
    var connetctionlist = new List<SlaveConnectionConfig>()
        {
            //第一个从库
            new SlaveConnectionConfig(){
                 HitRate=10,
                 //ConnectionString="Data Source=127.0.0.1;Initial Catalog=ctrip;User ID=sa;Password=sa123"  // SqlServer数据库
                 ConnectionString="server=127.0.0.1;uid=root;pwd=topsky;database=projectWebApi"  // mysql数据库
            }
        };

    ConnectionConfig connectionConfig1 = new ConnectionConfig()
    {
        DbType = DbType.MySql,  //数据库类型 ： .SqlServer、.MySql
        //ConnectionString = "Data Source=127.0.0.1;Initial Catalog=ctrip;User ID=sa;Password=123456", // SqlServer数据库
        ConnectionString = "server=127.0.0.1;uid=root;pwd=topsky;database=projectWebApi",  // mysql数据库
        InitKeyType = InitKeyType.Attribute,  //从特性读取主键和自增列信息
        //IsAutoCloseConnection = true,       //开启自动释放模式和EF原理一样
        //SlaveConnectionConfigs = connetctionlist
    };

    using (ISqlSugarClient client = new SqlSugarClient(connectionConfig1))
    {
        //一、基于数据库生成实体对象--DbFirst 
        {
            client.DbFirst.CreateClassFile(@"C:\workspace\NetCode\web\net6-web-api\Project.SqlSugarMigration\myModels");    
        }
    }
}
catch (Exception)
{

    throw;
}
