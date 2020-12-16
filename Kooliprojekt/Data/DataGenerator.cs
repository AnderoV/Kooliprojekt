using Kooliprojekt.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.Data
{
    public static class DataGenerator
    {
        public static void GenerateData(this ApplicationDbContext dbContext, int tenantId)
        {
            if(tenantId == 1)
            {
                GenerateForTenant1(dbContext, tenantId);
            }
            if (tenantId == 2)
            {
                GenerateForTenant2(dbContext, tenantId);
            }
            if (tenantId == 3)
            {
                GenerateForTenant3(dbContext, tenantId);
            }
        }

        private static void GenerateForTenant1(ApplicationDbContext dbContext, int tenantId)
        {

            var car = new Car();
            car.KmFare = 6;
            //car.TenantId = tenantId;

            dbContext.Add(car);
            dbContext.SaveChanges();


        }
        private static void GenerateForTenant2(ApplicationDbContext dbContext, int tenantId)
        {

            var car = new Car();
            car.KmFare = 4;
            //car.TenantId = tenantId;

            dbContext.Add(car);
            dbContext.SaveChanges();


        }
        private static void GenerateForTenant3(ApplicationDbContext dbContext, int tenantId)
        {

            var car = new Car();
            car.KmFare = 3;
            car.LicencePlate = "44545";
            //car.TenantId = tenantId;
            dbContext.Add(car);

            var car1 = new Car();
            car1.KmFare = 35;
            car1.LicencePlate = "44545fef";
            //car1.TenantId = tenantId;
            dbContext.Add(car1);



        }
    }
}
