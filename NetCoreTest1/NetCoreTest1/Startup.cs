using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace netcoretest
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}
        public static log4net.Repository.ILoggerRepository repository { get; set; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            repository = log4net.LogManager.CreateRepository("NETCoreRepository");
            log4net.Config.XmlConfigurator.Configure(repository, new System.IO.FileInfo("Config/log4net.config"));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc();

            services.AddMvc().ConfigureApplicationPartManager(m => {
                var feature = new Microsoft.AspNetCore.Mvc.Controllers.ControllerFeature();
                string pathDir = AppDomain.CurrentDomain.BaseDirectory;
                System.Reflection.Assembly controllerAssembly = null;
                //载入Controller配制循环载入第三方组件
                controllerAssembly = System.Reflection.Assembly.LoadFile(pathDir +@"NetCodeBus.dll");
                if (controllerAssembly!=null)
                { 
                    m.ApplicationParts.Add(new Microsoft.AspNetCore.Mvc.ApplicationParts.AssemblyPart(controllerAssembly));
                    m.PopulateFeature(feature);
                }

                controllerAssembly = System.Reflection.Assembly.LoadFile(pathDir + @"NetCodeBase.dll");
                if (controllerAssembly != null)
                {
                    m.ApplicationParts.Add(new Microsoft.AspNetCore.Mvc.ApplicationParts.AssemblyPart(controllerAssembly));
                    m.PopulateFeature(feature);
                }

                services.AddSingleton(feature.Controllers.Select(t => t.AsType()).ToArray());
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //      name:"test",
            //      template: "api/netcodeBase/{controller=Home}/{action=Index}/{id?}",
            //      defaults: new
            //      {
            //          namespaceName = "NetCodeBase.Controllers"
            //      }

            //        );
            //});
            //var log = log4net.LogManager.GetLogger(repository.Name, typeof(Startup));
            //log.Info("test");
        }

    }
}
