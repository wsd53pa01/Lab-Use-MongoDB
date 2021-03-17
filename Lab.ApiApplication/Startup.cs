using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Lab.ApiApplication.Model;
using Lab.Core.DIType;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

namespace Lab.ApiApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lab.ApiApplication", Version = "v1" });
            });

            #region MongoDB Injection
            var MongoDBConfiguration = Configuration.GetSection("MongoDB");
            DBSetting mongoDbSetting = MongoDBConfiguration.Get<DBSetting>();
            // Inject appsettings:ConnectionStrings
            services.AddSingleton<IMongoClient>(c =>
            {
                return new MongoClient(mongoDbSetting.ConnectionString);
            });

            services.AddScoped(c => 
                c.GetService<IMongoClient>().StartSession());

            List<Assembly> assemblies = new List<Assembly>();
            assemblies.Add(Assembly.Load("Lab.ApiApplication"));
            assemblies.Add(Assembly.Load("Lab.Repository"));
            assemblies.ForEach(a => 
            {
                var types = a.GetExportedTypes()
                        .Where(x => typeof(IModule).IsAssignableFrom(x) && x != typeof(IModule));
                foreach (var type in types)
                {
                    services.AddScoped(type.GetInterface($"I{type.Name}"), type);
                }
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lab.ApiApplication v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
