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
using QuickChain.Node.Utils;

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

        public IConfiguration Configuration { get; }

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
            services.AddTransient<IHashLibrary, HashLibrary>();
            services.AddTransient<INextBlockComposer, NextBlockComposer>();

            AddRepository<SignedTransaction>(services);
            AddRepository<Block>(services);
            AddRepository<Peer>(services);
            
            var sp = services.BuildServiceProvider();
            var repo = sp.GetService<IRepository<Block>>();
            var hashLib = sp.GetService<IHashLibrary>();
            var stRepo = sp.GetService<IRepository<SignedTransaction>>();
            services.AddSingleton<IMiningManager>(new MiningManager(repo, hashLib, stRepo));
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
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            this.EnsureMigration<QuickChainDbContext>(app);

            var blockRepo = app.ApplicationServices.GetRequiredService<IRepository<Block>>();
            this.SeedData(blockRepo);

            var miningManager = app.ApplicationServices.GetRequiredService<IMiningManager>();
            miningManager.Start();
        }

        private void EnsureMigration<T>(IApplicationBuilder app) where T : DbContext
        {
            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<T>();
                dbContext.Database.EnsureCreated();
            }
        }

        private void SeedData(IRepository<Block> blockRepo)
        {
            var genesisBlock = new Block()
            {
                Difficulty = 0,
                Hash = "TODO",
                ParentHash = "GENESIS",
                TimeStamp = DateTime.UtcNow,
                Height = 1,
                Transactions = new SignedTransaction[]
                {
                    this.GenerateTransaction(),
                    this.GenerateTransaction(),
                    this.GenerateTransaction(),
                    this.GenerateTransaction(),
                }
            };

            blockRepo.Insert(genesisBlock);
            blockRepo.Save();
        }

        private SignedTransaction GenerateTransaction()
        {
            return new SignedTransaction()
            {
                BlockHeight = 1,
                Fee = 0,
                IsSuccessful = true,
                From = "TODO1",
                To = "TODO",
                SenderPublicKey = "TODO",
                SignatureR = "TODO",
                SignatureS = "TODO",
                TransactionIdentifier = Guid.NewGuid(),
                TransactionHash = "todo",
                Value = 1000,
            };
        }
    }
}
