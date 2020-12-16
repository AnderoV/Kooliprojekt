using System;
using System.Linq;
using Kooliprojekt;
using Kooliprojekt.Data;
using Kooliprojekt.Data.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreTests.IntegrationTests
{
    public class FakeStartup : Startup
    {
        public FakeStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);

            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var tenantProvider = serviceScope.ServiceProvider.GetService<ITenantProvider>();                
                ((FileTenantProvider)tenantProvider).SetHostname("localhost");
                var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                if(dbContext == null)
                {
                    throw new NullReferenceException("Cannot get instance of dbContext");
                }

                if (!dbContext.Database.GetDbConnection().ConnectionString.ToLower().Contains("mytestdb.db"))
                {
                    throw new Exception("LIVE SETTINGS IN TESTS!");
                }

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                var a = new CarModel { Mark = "a" };
                dbContext.Cars.Add(new Car { LicencePlate = "aaa", KmFare = 1, TimeFare = 1, CarModel = a });
                dbContext.SaveChanges();
            }
        }
    }
}