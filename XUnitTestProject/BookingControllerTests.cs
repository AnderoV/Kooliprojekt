using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.IntegrationTests
{
    [Collection("Sequential")]

    public class BookingControllerTests : TestBase
    {

        public BookingControllerTests(TestApplicationFactory<FakeStartup> factory) : base(factory)
        {

        }

        [Theory]
        [InlineData("/Bookings")]
        public async Task Get_AnonymousCanAccessCarModelsLinks(string url)
        {
            //Arrange 
            var client = Factory.CreateClient();

            //Act 
            var response = await client.GetAsync(url);

            //Assert 
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/Bookings")]
        public async Task Get_UserCanAccessCarModelsLinks(string url)
        {
            var provider = TestClaimsProvider.WithUserClaims();
            var client = Factory.CreateClientWithTestAuth(provider);

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/Bookings")]
        public async Task Get_AdminCanAccessCarModelsLinks(string url)
        {
            var provider = TestClaimsProvider.WithAdminClaims();
            var client = Factory.CreateClientWithTestAuth(provider);

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}

