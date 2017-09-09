using System;
using System.Linq;
using BLL;
using BE.AccountEntities;
using BE;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DAL;
using DAL.Loggers;
using DAL.Repositories;

namespace API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddTransient<IErrorLogger, ConsoleLogger>();
            services.AddTransient<IRepository<Account>, AccountRepository>();
            services.AddTransient<IComponent<Account>, AccountComponent>();
            services.AddTransient<IRepository<Site>, SiteRepository>();
            services.AddTransient<IComponent<Site>, SiteComponent>();


            LookupValues.MONGODB_CONNECTION_STRING = Configuration.GetSection("MongoDb").GetChildren().Where(x => x.Key == "ConnectionString").FirstOrDefault().Value.ToString();
            LookupValues.MONGODB_COLLECTION_NAME = Configuration.GetSection("MongoDb").GetChildren().Where(x => x.Key == "CollectionName").FirstOrDefault().Value.ToString();
            
            var origins = Configuration.GetSection("CorsOrigins").GetChildren().Select(x => x.Value).ToArray();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder
                        .WithOrigins(origins)
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
            app.UseCors("AllowSpecificOrigin");
        }
    }
}
