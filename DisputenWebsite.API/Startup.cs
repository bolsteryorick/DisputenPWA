using AutoMapper;
using DisputenPWA.API.Extensions;
using DisputenPWA.Application;
using DisputenPWA.DAL.Models;
using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.EventAggregate.DALObject;
using DisputenPWA.Domain.EventAggregate.Mappers;
using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.GroupAggregate.DALObject;
using DisputenPWA.Domain.GroupAggregate.Mappers;
using DisputenPWA.Infrastructure.Connectors.AppEvents;
using DisputenPWA.Infrastructure.Connectors.Groups;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AppEventMapper());
                mc.AddProfile(new GroupMapper());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMediatr();
            services.AddGraphQLConfiguration();
            services.AddControllers();
            services.AddDbContext<DisputenAppContext>(options =>
                options.UseSqlServer(_configuration.GetValue<string>("DatabaseConnectionString")));

            services.AddTransient<IRepository<DALGroup>, Repository<DALGroup>>();
            services.AddTransient<IGroupConnector, GroupConnector>();

            services.AddTransient<IRepository<DALAppEvent>, Repository<DALAppEvent>>();
            services.AddTransient<IAppEventConnector, AppEventConnector>();

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
