using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Havana500.DataAccess.Contexts;
using Havana500.DataAccess.Extensions;
using Havana500.Domain;
using Havana500.Services;
using AutoMapper;
using Havana500.Business.Extensions;
using Havana500.Config;
using Swashbuckle.AspNetCore.Swagger;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Services;
using Hangfire;
using Hangfire.Common;
using Havana500.DataAccess.Jobs;
using Hangfire.SqlServer;

namespace Havana500
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
            var autoMapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfig());
            });

            var mapper = autoMapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            var migrationAssembly = "Havana500";

            services.AddDbContext<Havana500DbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), 
                builder => builder.MigrationsAssembly(migrationAssembly) ));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<Havana500DbContext>()
                .AddDefaultTokenProviders();

            //services.AddTransient<DbContextOptions<Havana500DbContext>>(_ =>
            //{
            //    var optionBuilder =  new DbContextOptionsBuilder<Havana500DbContext>();
            //    optionBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

            //    return optionBuilder.Options;
            //});
            #region SET CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllMethodsAndHeaders",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });
            #endregion

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    // base-address of your identityserver
                    options.Authority = "http://localhost:44365";

                    // name of the API resource
                    options.ApiName = "api1";

                    options.RequireHttpsMetadata = false;

                    options.EnableCaching = true;
                    options.SaveToken = true;

                }).AddJwtBearer("JwtBearer", options =>
                {
                    options.Authority = "http://localhost:44365";
                    options.RequireHttpsMetadata = false;

                    options.SaveToken = true;

                    options.Audience = "http://localhost:44365/resources";

                });

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
                .AddInMemoryClients(IdentityServerConfig.GetClients())
                .AddTestUsers(IdentityServerConfig.GetUsers())
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddAspNetIdentity<ApplicationUser>()
                .Services.AddTransient<IProfileService, IdentityProfileService>();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            //adds the configuration of the services for the DataLayer
            services.AddDataAccessServices();

            //adds the configuration of the services for the BusinessLayer
            services.AddBusinessServices();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc();

            services.AddHangfire(configuration =>
            {
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Havana500 Api", Version = "v1" });
            });

            //configure background jobs
            var methodInfo = typeof(ArticleBackgroundJobs).GetMethod("UpdateWeightColumn");
            var job = new Job(typeof(ArticleBackgroundJobs), methodInfo);

            GlobalConfiguration.Configuration.UseActivator(new ContainerJobActivator(new ServiceContainer()));
            JobStorage.Current = new SqlServerStorage(Configuration.GetConnectionString("DefaultConnection"));

            var recurringJobManager = new RecurringJobManager(JobStorage.Current);
            recurringJobManager.AddOrUpdate("job_update_weight_in_articles", job, Cron.Minutely());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();

                //TODO: have to try to resolve this in the library. Maybe is the SeedEnginge is configured
                //in the ConfigureServices, this will be possible ;)
               // var context = app.ApplicationServices.GetRequiredService<Havana500DbContext>();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            #region configure Hangfire

            GlobalConfiguration.Configuration.UseSqlServerStorage(
                Configuration.GetConnectionString("DefaultConnection"));
            app.UseHangfireDashboard();
            app.UseHangfireServer();

            #endregion

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseCors("AllowAllMethodsAndHeaders");

            app.UseIdentityServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Havana500 Api");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
