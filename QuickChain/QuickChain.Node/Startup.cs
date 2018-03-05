using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuickChain.Data;
using QuickChain.Model;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;

namespace QuickChain.Node
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

        public void ConfigureServices(IServiceCollection services)
        {
            string dbName = string.Format("Data Source=quickchain_{0}.db", Guid.NewGuid().ToString("N"));
            services.AddDbContext<QuickChainDbContext>(options =>
                options.UseSqlite(dbName));

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "QuickChain Node", Version = "v1" });
            });

            services.AddDistributedMemoryCache();

            // Add application services.
            AddRepository<Transaction>(services);
            AddRepository<Block>(services);
        }

        private static void AddRepository<T>(IServiceCollection services) where T : Entity
        {
            services.AddScoped<IRepository<T>, Repository<T>>(
                s => new Repository<T>(s.GetRequiredService<QuickChainDbContext>())
            );
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuickChain Node v1");
            });

            app.UseDeveloperExceptionPage();
            app.UseMvc();

            this.EnsureMigration<QuickChainDbContext>(app);
        }

        public void EnsureMigration<T>(IApplicationBuilder app) where T : DbContext
        {
            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<T>();
                dbContext.Database.Migrate();
            }
        }
    }
}
