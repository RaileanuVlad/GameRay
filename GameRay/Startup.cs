using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using GameRay.Contexts;
using GameRay.Helpers;
using GameRay.Repositories.UserRepository;
using GameRay.Repositories.AdminRepository;
using GameRay.Repositories.CategoryRepository;
using GameRay.Repositories.DeveloperRepository;
using GameRay.Repositories.UserGameRepository;
using GameRay.Repositories.GameCategoryRepository;
using GameRay.Repositories.GameRepository;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Hosting;

namespace GameRay
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddRazorPages();
            services.AddCors();
            services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserGameRepository, UserGameRepository>();
            services.AddTransient<IGameRepository, GameRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IGameCategoryRepository, GameCategoryRepository>();
            services.AddTransient<IDeveloperRepository, DeveloperRepository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "GameRay API",
                    Version = "",
                    Description = "API for university project GameRay",
                    Contact = new OpenApiContact
                    {
                        Name = "Raileanu Vlad",
                        Email = string.Empty
                    },
                });
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseMiddleware<JwtMiddleware>();
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zomato API V1");

                c.RoutePrefix = string.Empty;
            });
            app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials()
            );


            app.UseStaticFiles();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });





        }
    }
}