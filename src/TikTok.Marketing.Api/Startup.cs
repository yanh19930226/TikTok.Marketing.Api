using Core;
using Core.Logger;
using Core.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using TikTok.Marketing.Api.Sdk;

namespace TikTok.Marketing.Api
{
    public class Startup : CommonStartup
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {

        }

        public override void CommonServices(IServiceCollection services)
        {
            services.AddDbContext<TikTokContext>(options =>
            {
                options.UseMySql(Configuration.GetSection("Zeus:Connection").Value, sql => sql.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name));
            });

            #region 客户端
            services.Configure<Appsettings>(Configuration.GetSection("Appsettings"));

            var settings = services.BuildServiceProvider().GetService<IOptions<Appsettings>>().Value;

            var client = new HttpClient();

            services.AddSingleton(new TikTokClient(settings.TikTokConfig.IsDev ? EnvEnum.Dev : EnvEnum.Prod, client)); 
            #endregion

            #region  ToDo 批量注入Services
            //services.AddScoped<IAdAccountService, AdAccountService>();
            //services.AddScoped<IBusinessManagerService, BusinessManagerService>();
            //services.AddScoped<IBusinessAssetService, BusinessAssetService>();
            //services.AddScoped<ISystemUserService, SystemUserService>();//IBusinessManagerService
            //services.AddScoped<IBusinessManagerService, BusinessManagerService>();
            #endregion

            services.AddCoreSwagger()
                        .AddCoreSeriLog();
        }

        public override void CommonConfigure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCoreSwagger();
        }
    }
}
