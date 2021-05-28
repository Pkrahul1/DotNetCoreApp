using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreAPIEmpty.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CoreAPIEmpty
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();//AddMvcCore adds only core service so if index from home controller return json, it will not
            //be handled , addMvc add all the services,it internaly calls AddMvcCore also.
            services.AddSingleton<IEmployeeRepository, EmployeeManagement>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions
                {
                    SourceCodeLineCount=1
                };

                app.UseDeveloperExceptionPage(developerExceptionPageOptions);//this middleware detect the exception and pass reuest to next middle ware if
                //any in the pipeline.it sud be given as early as possible 
            }
            #region static files
            //all static files sud ne in wwwrott folder and use below middelwares. and nevigate to the file path in browser
            //when correct path is given in nrowser below middleware will handle the request and as middlewarein in app.run is
            //not called request will not go there , this middleware will produce response . 
            //if this middle ware will be given after app.run middle ware request wont be going there as  middleware inside
            //app.run cant use next();
            #region only static
            //app.UseStaticFiles();//use only this and nevigate to path.
            #endregion
            //DefaultFilesOptions defaultFilesOption = new DefaultFilesOptions();
            //defaultFilesOption.DefaultFileNames.Clear();
            //defaultFilesOption.DefaultFileNames.Add("myInfo.html");
            //app.UseDefaultFiles(defaultFilesOption);//this middle ware used to load(loaded on root url) default static file of application with name default or index
            //it should be above usestatic files as it only changes the path to defaul or index file and usedtaticfile middleware display it.
            //app.UseStaticFiles();
            FileServerOptions fileServerOption = new FileServerOptions();
            fileServerOption.DefaultFilesOptions.DefaultFileNames.Clear();
            fileServerOption.DefaultFilesOptions.DefaultFileNames.Add("info.html");
            //app.UseFileServer(fileServerOption);
            app.UseMvcWithDefaultRoute();//this will execute index method in home controller by default.if this doesn't handle
            //request will pass request to next MW.if handles the req , will reverse the pipeline
            #endregion

            #region middleware calls
            //app.Use(async (context, next) =>
            //{
            //    logger.LogInformation("inside 1st middleware before next-->");
            //    await context.Response.WriteAsync("inside 1st middleware before next-->");
            //    await next();
            //    await context.Response.WriteAsync("inside 1st middleware after next<--");
            //    logger.LogInformation("inside 1st middleware after next-->");
            //});
            //app.Use(async (context, next) =>
            //{
            //    logger.LogInformation("inside 2nd middleware before next-->");
            //    await context.Response.WriteAsync("inside 2nd middleware before next-->");
            //    await next();
            //    await context.Response.WriteAsync("inside 2nd middleware after next<--");
            //    logger.LogInformation("inside 2nd middleware after next-->");
            //});
            #endregion

            app.Run(async (context) =>
            {
                // throw new Exception("Exception from last middleware");
                await context.Response.WriteAsync(env.EnvironmentName);
                await context.Response.WriteAsync("inside last middle ware..");
                await context.Response.WriteAsync("inside last middleware after response<--");
                //logger.LogInformation("inside last middleware before next-->");
                //await context.Response.WriteAsync(_config["myKey"]);
                //await context.Response.WriteAsync(System.Diagnostics.Process.GetCurrentProcess().ProcessName);
                //await context.Response.WriteAsync("Hello Worldd!");
            });
        }
    }
}
