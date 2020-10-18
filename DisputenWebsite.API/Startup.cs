using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DisputenPWA.API.Extensions;
using DisputenPWA.Application;
using DisputenPWA.DAL.Models;
using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.WeatherAggregate;
using DisputenPWA.Infrastructure.Connectors.Groups;
using DisputenPWA.Infrastructure.SqlDatabase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DisputenWebsite.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatr();
            services.AddGraphQLConfiguration();
            services.AddControllers();
            services.AddDbContext<DisputenAppContext>(options =>
                options.UseSqlServer(_configuration.GetValue<string>("DatabaseConnectionString")));
            services.AddTransient<IRepository<WeatherForecast>, Repository<WeatherForecast>>();
            services.AddTransient<IRepository<Group>, Repository<Group>>();
            services.AddTransient<ISqlDatabaseConnector, SqlDatabaseConnector>();
            services.AddTransient<IGroupConnector, GroupConnector>();
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGraphQL();

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
