using DisputenPWA.API.Authoriation;
using DisputenPWA.API.Extensions;
using DisputenPWA.Application;
using DisputenPWA.Application.Extensions;
using DisputenPWA.Application.Services;
using DisputenPWA.DAL.Models;
using DisputenPWA.Domain.Aggregates.ContactAggregate.Commands;
using DisputenPWA.Domain.Aggregates.UserAggregate.DalObject;
using DisputenPWA.Infrastructure.Extensions;
using DisputenPWA.SQLResolver.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
            services.AddApplicationMediatr();
            services.AddInfratructureMediatr();
            services.AddGraphQLConfiguration();
            services.AddControllers();
            services.AddDbContext<DisputenAppContext>(options =>
                options.UseSqlServer(_configuration.GetValue<string>("DatabaseConnectionString")));
            services
                .AddDefaultIdentity<ApplicationUser>(
                    options => options.SignIn.RequireConfirmedAccount = false
                    )
                .AddEntityFrameworkStores<DisputenAppContext>();
            services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Lockout.AllowedForNewUsers = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });
            //services.AddCosmosDb(_configuration);

            services.AddTransient<IUserService, UserService>();
            services.AddSQLConnectors();
            services.AddSQLResolverServices();
            services.AddBusinessLogicServices();

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
            //app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowAnyCredentials());
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseAuthorization();
            app.UseMiddleware<JwtMiddleware>();
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
