using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Mvc;
using PropertyExchange.Presentation.API.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PropertyExchange.Presentation.API.Automapper;
using PropertyExchange.Core.Domain.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using PropertyExchange.Infrastructure.Data.Common;
using PropertyExchange.Core.Application.Contracts;
using PropertyExchange.Core.Application.UseCases;
using PropertyExchange.Core.Application.Repositories;
using PropertyExchange.Infrastructure.Data.Repositories;
using System.Text.Json.Serialization;

namespace PropertyExchange.Presentation.Api
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
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            ////Jwt Authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Key);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc(setupAction =>
            {
                setupAction.EnableEndpointRouting = false;
            }).AddJsonOptions(jsonOptions =>
            {
                jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
            })
          .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddDbContext<PEDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("PEDbContextConnectionString"), new MySqlServerVersion(new Version(8, 0, 35)));
            });


            //services.AddHttpContextAccessor();

            services.AddScoped<PEDbContext>();
            //USER
            services.AddScoped<IUserRegistrationLoginUseCase, UserRegistrationLoginUseCase>();
            services.AddScoped<IUserRegistrationLoginRepo, UserRegistrationLoginRepo>();
            services.AddScoped<IUserUseCase, UserUseCase>();
            services.AddScoped<IUserRepo, UserRepo>();

            //PROPERTY
            services.AddScoped<IPropertyUseCase, PropertyUseCase>();
            services.AddScoped<IPropertyRepo, PropertyRepo>();

            //TENANT
            services.AddScoped<ITenantUseCase, TenantUseCase>();
            services.AddScoped<ITenantRepo, TenantRepo>();

            services.AddAutoMapper(typeof(AutomapperAPIProfile));
            services.AddEndpointsApiExplorer();
            //services.AddSwaggerGen();

            services.AddControllersWithViews();

            services.AddCors();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMapper automapper)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Handle exceptions in a more generic way for non-development environments
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }


            automapper.ConfigurationProvider.AssertConfigurationIsValid();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();


            app.UseCors(builder =>
               builder
                   .AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



        }
    }
}
