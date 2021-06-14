namespace SberCloudHack
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using DogGrooming.Web.impl;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;

    using SberCloudHack.Data;
    using SberCloudHack.Hubs;

    public class Startup
    {

        private bool useSwagger;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            this.useSwagger = true;// TDOD
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
            services.AddSignalR();

            services.AddOptions();
            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            services.AddControllers();

            if (this.useSwagger)
            {
                services.AddSwaggerGen(x =>
                {
                    foreach (var xmlPath in Directory.EnumerateFiles(
                        AppContext.BaseDirectory, "*.xml"))
                    {
                        x.IncludeXmlComments(xmlPath, true);
                    }

                    x.AddSecurityDefinition(
                        JwtBearerDefaults.AuthenticationScheme,
                        new OpenApiSecurityScheme
                        {
                            In = ParameterLocation.Header,
                            Description
                                = "Enter JWT Bearer token **_only_**",
                            Name = "JWT Authentication",
                            Type = SecuritySchemeType.Http,
                            Scheme = "bearer",
                            BearerFormat = "JWT"
                        });

                    x.OperationFilter<SecurityRequirementsOperationFilter>();
                });
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();

            if (this.useSwagger)
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json",
                        "SecurityBand Chat API V1.00");
                });
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chat");
            });
        }
    }
}
