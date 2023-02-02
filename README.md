# Net6WebApi Demo

#### 一、开发环境
1.1 项目开发工具
  + 开发工具：Visual Studio 2022
  + 采用技术：.NET6.0 WebApi + mysql + Redis + ORM(SqlsugarCore) + DbFirst + AOP + Autofac + AutoMapper + JWT + log4net + Swagger 
	
1.2 代码git clone
  + gitHub clone仓库代码：https://github.com/xiehuihuang/net6-web-api.git
  + gitee  clone仓库代码：https://gitee.com/jack2490/net6-web-api.git


#### 二、搭建分层架构 表现层--服务层（业务逻辑层抽象、业务逻辑层实现）---数据访问
~~~~
Project.WebApi  ：        表现层--UI层
Project.Interfaces：      服务层--业务逻辑层抽象
Project.Services:         服务层--业务逻辑层实现
Project.DbModels          数据库实体对象
Project.DTO               数据传输对象，主要用于数据传递，是面向界面UI，实现表现层和业务层的解耦
Project.Infrastructure    基础设施层--主要存储一些公共的基础类库和组件、属于所有层的最底层，可以被任何层都引用

SqlSugar：                数据访问层
 
通过SqlSugarCore 基于数据库生成实体对象
创建项目：Project.SqlSugarMigration ---专门用来放从数据库中生成和表对应的实体的工具；


三层架构完
~~~~

#### 业务功能模块

一、用户模块
  + 1.注册(发送短信验证码、注册用户）
  + 2.登录(手机号登录(验证码)、用户名密码登录)
  + 3.上传文件
  + 4.获取用户信息、修改用户信息(JWT鉴权)

#### 使用说明
1. SwaggerExtend.cs         --- Swagger配置封装
2. AutofaceExtend.cs        --- 替换Autofac 业务逻辑层抽象和服务的注册
3. CorsExtend.cs            --- 跨域策略配置封装
4. AuthorizationExtend.cs   --- JWT授权封装
5. HSJWTService.cs          --- JWT对称可逆加密
6. RSSJWTervice.cs          --- JWT非对称可逆加密
7. AutoMapObject.cs         --- 支持Automapper映射
![image](https://user-images.githubusercontent.com/32085450/216363378-d631b253-6f41-44f7-9ba5-5f632936b46e.png)



#### 特技

1.  使用 Readme\_XXX.md 来支持不同的语言，例如 Readme\_en.md, Readme\_zh.md
2.  Gitee 官方博客 [blog.gitee.com](https://blog.gitee.com)
3.  你可以 [https://gitee.com/explore](https://gitee.com/explore) 这个地址来了解 Gitee 上的优秀开源项目
4.  [GVP](https://gitee.com/gvp) 全称是 Gitee 最有价值开源项目，是综合评定出的优秀开源项目
5.  Gitee 官方提供的使用手册 [https://gitee.com/help](https://gitee.com/help)
6.  Gitee 封面人物是一档用来展示 Gitee 会员风采的栏目 [https://gitee.com/gitee-stars/](https://gitee.com/gitee-stars/)
