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
using Diamonds.Common.GameEngine.GameObjects;
using Diamonds.GameEngine;
using Diamonds.Common.GameEngine.Move;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;

namespace Diamonds.Rest
{
    public class Startup
    {
        private const string ApiName = "Etimo Diamonds API";
        private const string VersionString = "v1";

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
            services.AddScoped<IGameObjectGeneratorService, GameObjectGeneratorService>();

            // Add framework services.
            services.AddMvc();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(VersionString, new Info { Title = ApiName, Version = VersionString });

                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Diamonds.Rest.xml");
                c.IncludeXmlComments(filePath);
                
                c.DescribeAllEnumsAsStrings();
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
                c.SwaggerEndpoint($"/api/swagger/{VersionString}/swagger.json", ApiName);
                c.RoutePrefix = "api/docs";
            });
        }
    }
}
