using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VicSites.Business.Definition;
using VicSites.Business.Implementation;
using VicSites.Common.Infrastructure;

namespace VicSites.Api
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
            services.AddLogging(configure =>
            {
                configure.SetMinimumLevel(LogLevel.Warning);
            });

            SetupRsedis(services);

            services.AddScoped<IMainComponent, MainComponent>();
        }

        private void SetupRedis(IServiceCollection services)
        {
            var redisAddress = EnvironmentSettings.GetEnvironmentVariable("REDIS_ADDRESS", "192.168.99.100:6379");
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = redisAddress;
                options.InstanceName = "master";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
