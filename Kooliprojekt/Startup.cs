using AutoMapper;
using Kooliprojekt.Data;
using Kooliprojekt.Data.Extensions;
using Kooliprojekt.MappingProfiles;
using Kooliprojekt.Models;
using Kooliprojekt.ServiceClasses;
using Kooliprojekt.ServiceInterfaces;
using Kooliprojekt.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Input;

namespace Kooliprojekt
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => 
            {
               // options.UseSqlite("Data source=mydb.db");
            });

            services.AddScoped<IFileClient>(factory =>
            {
                var rootPath = "wwwroot/pictures";
                return new LocalFileClient(rootPath);
            });

            services.AddScoped<IFileClient, AzureBlobFileClient>(client =>
            {
                var cloudConnStr = Configuration["StorageConnectionString"];
                return new AzureBlobFileClient(cloudConnStr);
            });

            services.AddAutoMapper(typeof(Program).Assembly);

            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;

            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddRazorPages();
            services.AddMvc().AddRazorRuntimeCompilation();
           
            services.AddScoped<ITenantProvider, FileTenantProvider>();
            services.AddScoped<CompositeCommand>();
            services.AddScoped<SaveBookingToDatabaseCommand>();
            services.AddScoped<Command1>();
            services.AddScoped<Command2>();
            services.AddScoped<ICarService,CarService>();
            services.AddScoped<ICarModelService, CarModelService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IOperationService, OperationService>();
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseMiddleware<MissingTenantMiddleware>(Configuration["MissingTenantUrl"]);

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapRazorPages();
            });

            //var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            //using (var serviceScope = serviceScopeFactory.CreateScope())
            //{
            //    var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            //    dbContext.Database.EnsureCreated();

            //    if (dbContext.Roles.Count() == 0)
            //    {
            //        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            //        var role = new IdentityRole { Id = "Admin", Name = "Admin" };

            //        roleManager.CreateAsync(role).GetAwaiter().GetResult();

            //        // Lisa admin 
            //        var user = new IdentityUser
            //        {
            //            Email = "test@example.com",
            //            Id = Guid.NewGuid().ToString(),
            //            EmailConfirmed = true,
            //            NormalizedEmail = "test@example.com",
            //            UserName = "test@example.com"
            //        };

            //        userManager.CreateAsync(user, "K33ruline!!!").GetAwaiter().GetResult();
            //        userManager.AddToRoleAsync(user, "Admin");

            //        // Lisa tavakasutaja 
            //        user = new IdentityUser
            //        {
            //            Email = "dumbuser@example.com",
            //            Id = Guid.NewGuid().ToString(),
            //            EmailConfirmed = true,
            //            NormalizedEmail = "dumbuser@example.com",
            //            UserName = "dumbuser@example.com"
            //        };
            //        userManager.CreateAsync(user, "K33ruline!!!").GetAwaiter().GetResult();
            //    }
            //}
            var options = new DbContextOptions<ApplicationDbContext>();
            var provider = new FileTenantProvider();

            foreach (var tenant in provider.ListTenants())
            {
                provider.SetHostname(tenant.Host);

                using (var dbContext = new ApplicationDbContext(options, provider))
                {
                    dbContext.Database.EnsureCreated();
                    dbContext.GenerateData(tenant.Id);
                    /*
                    if (dbContext.Cars.Count(p => p.TenantId == tenant.Id) == 0)
                    {
                        dbContext.GenerateData(tenant.Id);
                        dbContext.SaveChanges();
                    }
                    Debug.WriteLine("Products: " + dbContext.Cars.Count());
                    */
                }
            }
        }
    }
}