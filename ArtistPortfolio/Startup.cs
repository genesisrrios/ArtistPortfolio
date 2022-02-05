using AutoMapper;
using DAL;
using DAL.Interfaces;
using DAL.Repository;
using DAL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace ArtistPortfolio
{
    public class Startup
    {
        private readonly IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ArtistDatabaseSettings>(Configuration.GetSection("ArtistDatabaseSettings"));
            IMapper mapper = new AutoMapperConfig().Configure().CreateMapper();
            services.AddSingleton(mapper);
            services.AddSingleton<IArtistDatabaseSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<ArtistDatabaseSettings>>().Value);
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            services.AddScoped<ArtistService>();
            services.AddScoped<AdminService>();
            services.AddScoped<Security>();
            services.AddTransient<ITokenService, TokenService>();

            services.AddSession();
            services.ConfigureJwtAuthentication(Configuration);
            services.AddRazorPages().AddSessionStateTempDataProvider();
            services.AddServerSideBlazor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.EnvironmentName == "Development")
            {
                    app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSession();
            app.UseAuthentication();
            app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("Token");
                if (!String.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
                await next();
            });
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapRazorPages();
                    endpoints.MapBlazorHub();
                    endpoints.MapFallbackToPage("/_Host");
                });
        }
    }
}
