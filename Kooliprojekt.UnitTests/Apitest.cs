using Kooliprojekt.Controllers;
using Kooliprojekt.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooliprojekt.UnitTests
{
    class Apitest : TestBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ApiController API;

        public Apitest()
        {
            API = new ApiController(dbContext);
            dbContext = GetDbContext();
        }


    }
}
