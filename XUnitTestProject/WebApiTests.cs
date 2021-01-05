using AspNetCoreTests.IntegrationTests;
using Kooliprojekt.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Kooliprojekt.IntegrationTests
{
    [Collection("Sequential")]
    public class WebApiTests : TestBase
    {
        public WebApiTests(TestApplicationFactory<FakeStartup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Create_EndpointRetrunSuccessForCorrectModel()
        {
            var claimsProvider = TestClaimsProvider.WithAdminClaims();
            var client = Factory.CreateClientWithTestAuth(claimsProvider);


            var car = new Car { LicencePlate = "455", KmFare = 65, TimeFare = 56 };
            var stringContent = new StringContent(JsonConvert.SerializeObject(car), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api", stringContent);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        }

        [Fact]
        public async Task edit_EndpointRetrunSuccessForCorrectModel()
        {
            var claimsProvider = TestClaimsProvider.WithAdminClaims();
            var client = Factory.CreateClientWithTestAuth(claimsProvider);


            var car = new Car { LicencePlate = "455", KmFare = 65, TimeFare = 56 };
            var stringContent = new StringContent(JsonConvert.SerializeObject(car), Encoding.UTF8, "application/json");
            var response = await client.PutAsync("/api/" + car.Id.ToString(), stringContent);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        }



    }
}
