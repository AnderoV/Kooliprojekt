using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Kooliprojekt.Data;
using Kooliprojekt.Data.Extensions;
using Kooliprojekt.MappingProfiles;
using Microsoft.EntityFrameworkCore;

namespace Kooliprojekt.UnitTests
{
    public abstract class TestBase
    {
        protected readonly IMapper Mapper;

        public TestBase()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddMaps(typeof(BookingProfile).Assembly);
            });

            Mapper = mappingConfig.CreateMapper();
        }

        protected ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                  .Options;
            var tenantProvider = new FakeTenantProvider();

            return new ApplicationDbContext(options, tenantProvider);
        }
    }
}