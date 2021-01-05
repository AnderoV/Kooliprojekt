using AspNetCoreTests.IntegrationTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Kooliprojekt.IntegrationTests
{
    [Collection("Sequential")]

    public class OperationsControllerTests : TestBase
    {

        public OperationsControllerTests(TestApplicationFactory<FakeStartup> factory) : base(factory)
        {

        }

        [Theory]
        [InlineData("/Operations")]
        public async Task Get_AnonymousCanAccessOperationLinks(string url)
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
        [InlineData("/Operations")]
        public async Task Get_UserCanAccessOperationLinks(string url)
        {
            var provider = TestClaimsProvider.WithUserClaims();
            var client = Factory.CreateClientWithTestAuth(provider);

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/Operations")]
        public async Task Get_AdminCanAccessOperationLinks(string url)
        {
            var provider = TestClaimsProvider.WithAdminClaims();
            var client = Factory.CreateClientWithTestAuth(provider);

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task Create_EndpointRetrunSuccessForCorrectModel()
        {
            var claimsProvider = TestClaimsProvider.WithAdminClaims();
            var client = Factory.CreateClientWithTestAuth(claimsProvider);

            var formValues = new Dictionary<string, string>();

            formValues.Add("Title", "title");
            formValues.Add("Desc", "desc");
            formValues.Add("CarId", "2");

            var content = new FormUrlEncodedContent(formValues);
            var response = await client.PostAsync("Operations/Create", content);

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);

        }

        [Fact]
        public async Task Edit_EndpointRetrunSuccessForCorrectModel()
        {
            var claimsProvider = TestClaimsProvider.WithAdminClaims();
            var client = Factory.CreateClientWithTestAuth(claimsProvider);

            var formValues = new Dictionary<string, string>();

            formValues.Add("Title", "title");
            formValues.Add("Desc", "desc");
            formValues.Add("CarId", "2");

            var content = new FormUrlEncodedContent(formValues);
            var response = await client.PostAsync("Operations/Create", content);

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);

        }
    }
}

