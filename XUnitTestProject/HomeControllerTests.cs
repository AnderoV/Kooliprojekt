using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.IntegrationTests
{
    [Collection("Sequential")]

    public class HomeControllerTests : TestBase
    {

        public HomeControllerTests(TestApplicationFactory<FakeStartup> factory) : base(factory)
        {

        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Home/Privacy")]
        [InlineData("/CarModels")]
        [InlineData("/Cars")]
        [InlineData("/Bookings")]
        public async Task Get_AnonymousCanAccessHomeLinks(string url)
        {
            // Arrange 
            var client = Factory.CreateClient();
            // Act 
            var response = await client.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.Redirect)
            {
                throw new System.Exception(response.Headers.Location + " " + client.BaseAddress);
            }
            // Assert 
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Home/Privacy")]
        [InlineData("/CarModels")]
        [InlineData("/Cars")]
        [InlineData("/Bookings")]
        public async Task Get_UserCanAccessHomeLinks(string url)
        {
            var provider = TestClaimsProvider.WithUserClaims();
            var client = Factory.CreateClientWithTestAuth(provider);

            var response = await client.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.Redirect)
            {
                throw new System.Exception(response.Headers.Location + " " + client.BaseAddress);
            }

            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Home/Privacy")]
        [InlineData("/CarModels")]
        [InlineData("/Cars")]
        [InlineData("/Bookings")]
        public async Task Get_AdminCanAccessHomeLinks(string url)
        {
            var provider = TestClaimsProvider.WithAdminClaims();
            var client = Factory.CreateClientWithTestAuth(provider);

            var response = await client.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.Redirect)
            {
                throw new System.Exception(response.Headers.Location + " " + client.BaseAddress);
            }

            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}
