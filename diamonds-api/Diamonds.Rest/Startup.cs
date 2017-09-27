using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Swashbuckle.AspNetCore.Swagger;

using Diamonds.Common.Storage;
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


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Etimo Diamonds API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            app.UseSwagger(swaggerOptions => {
                swaggerOptions.RouteTemplate = "api/swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "Etimo Diamonds API V1");
                c.RoutePrefix = "api/docs";
            });
        }
    }
}
