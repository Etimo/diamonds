using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Diamonds.Common.Storage;
using System;
using Diamonds.Common.GameEngine.DiamondGenerator;
using Diamonds.GameEngine;
using Diamonds.Common.GameEngine.Move;

namespace Diamonds.Rest
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
            MongoDBStorage.ConnectionString = Configuration.GetValue<string>("MongoDb:ConnectionString");
            MongoDBStorage.DatabaseName = Configuration.GetValue<string>("MongoDb:DatabaseName");
            MongoDBStorage.IsSSL = Convert.ToBoolean(Configuration.GetValue<bool>("MongoDb:IsSSL"));

            services.AddScoped<IStorage, MongoDBStorage>();
            services.AddScoped<IMoveService, MoveService>();
            services.AddScoped<IDiamondGeneratorService, DiamondGeneratorService>();

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
