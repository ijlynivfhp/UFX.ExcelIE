using AutoMapper;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFX.ExcelIE.Domain;
using UFX.Common.Domain;
using UFX.EntityFrameworkCore.UnitOfWork;
using UFX.Infra.Extensions;
using UFX.Redis.Extensions;

namespace UFX.ExcelIE.HttpApi.Client
{
    /// <summary>
    /// 配置读取
    /// </summary>
    public static class ConfigureServices
    {
        /// <summary>
        /// 读取配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddAppConfigures(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMyCapConfigures(configuration);
            services.AddMyDbContextConfigures(configuration);
            services.AddRedisConfigures(configuration);
            services.AddSwaggerConfigures();
            return services;
        }

        /// <summary>
        /// 配置cap
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMyCapConfigures(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCap(x =>
            {
                //配置Cap的本地消息记录库，用于服务端保存Published消息记录表；客户端保存Received消息记录表

                // 此方法默认使用的数据库Schema为Cap；2，要求最低sql server2012(因为使用了Dashboard的sql查询语句使用了Format新函数)
                x.UseSqlServer(configuration.GetConnectionString("CAPConnection"));

                // 配置Cap的本地消息记录库，用于服务端保存Published消息记录表；客户端保存Received消息记录表
                // 此方法可以指定是否使用sql server2008,数据库Schema,链接字符串
                //x.UseSqlServer((options) =>
                //{
                //    //数据库连接字符串
                //    options.ConnectionString = "Integrated Security=False;server=192.168.1.109;database=cap;User ID=sa;Password=密码;Connect Timeout=30";
                //    //标记使用的是SqlServer2008引擎(此处设置的是2008,因为192.168.1.109数据库是2008)
                //    options.UseSqlServer2008();
                //    //Cap默认使用的数据库Schema为Cap;此处可以指定使用自己的数据库Schema
                //    //options.Schema = "dbo";
                //});
                #region 使用rabbit作为底层之间的消息发送
                //使用rabbit作为底层之间的消息发送
                x.UseRabbitMQ(o =>
                {
                    o.HostName = configuration.GetSection("RabbitMQ:HostName").Value;
                    o.UserName = configuration.GetSection("RabbitMQ:UserName").Value;
                    o.Password = configuration.GetSection("RabbitMQ:Password").Value;
                    o.Port = configuration.GetSection("RabbitMQ:Port").Value.ToInt();
                });
                ////设置成功的数据在数据库保存的天数（秒单数）
                //if (configuration.GetSection("RabbitMQ:SucceedMessageExpiredAfter").Value.ToInt() > 0)
                //    x.SucceedMessageExpiredAfter = configuration.GetSection("RabbitMQ:SucceedMessageExpiredAfter").Value.ToInt();
                ////设置失败重试的次数
                //if (configuration.GetSection("RabbitMQ:FailedRetryCount").Value.ToInt() > 0)
                //    x.FailedRetryCount = configuration.GetSection("RabbitMQ:FailedRetryCount").Value.ToInt();

                #endregion

                //使用Dashboard，这是一个Cap的可视化管理界面；默认地址:http://localhost:端口/cap
                x.UseDashboard();

                //默认分组名，此值不配置时，默认值为当前程序集的名称
                //x.DefaultGroup = "m";
                //失败后的重试次数，默认50次；在FailedRetryInterval默认60秒的情况下，即默认重试50*60秒(50分钟)之后放弃失败重试
                //x.FailedRetryCount = 10;

                //失败后的重拾间隔，默认60秒
                //x.FailedRetryInterval = 30;

                //设置成功信息的删除时间默认24*3600秒
                //x.SucceedMessageExpiredAfter = 60 * 60;
            });
            return services;
        }

        /// <summary>
        /// 配置数据库
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMyDbContextConfigures(this IServiceCollection services, IConfiguration configuration)
        {
            //SCMContext 为实际实体对应的上下文
            services.AddDbContext<Domain.SCMContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("SCMConnection"))).AddUnitOfWork();

            services.AddUnitOfWork(services.BuildServiceProvider().GetRequiredService<IMapper>());
            return services;
        }

        public static IServiceCollection AddRedisConfigures(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRedisRepository(configuration.GetSection("RedisConfig"));
            return services;
        }
        /// <summary>
        /// 配置swagger
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerConfigures(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DemoApi v1", Version = "v1" });
                c.AddServer(new OpenApiServer()
                {
                    Url = "",
                    Description = "领猫科技DemoApi文档"
                });
                c.CustomOperationIds(apiDesc =>
                {
                    var controllerAction = apiDesc.ActionDescriptor as ControllerActionDescriptor;
                    return controllerAction.ControllerName + "-" + controllerAction.ActionName;
                });

                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "UFX.ExcelIE.HttpApi.xml"), true);
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "UFX.ExcelIE.Domain.Shared.xml"), true);
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "UFX.ExcelIE.Application.Contracts.xml"), true);
            });

            return services;
        }
    }
}
