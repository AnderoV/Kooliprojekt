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

    public class CarControllerTests : TestBase
    {

        public CarControllerTests(TestApplicationFactory<FakeStartup> factory) : base(factory)
        {

        }

        [Theory]
        [InlineData("/Cars")]
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
        [InlineData("/Cars")]
        public async Task Get_UserCanAccessCarModelsLinks(string url)
        {
            var provider = TestClaimsProvider.WithUserClaims();
            var client = Factory.CreateClientWithTestAuth(provider);

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/Cars")]
        public async Task Get_AdminCanAccessCarModelsLinks(string url)
        {
            var provider = TestClaimsProvider.WithAdminClaims();
            var client = Factory.CreateClientWithTestAuth(provider);

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/Cars/Edit/")]
        [InlineData("/Cars/Edit/1")]
        public async Task Get_EditReturnsFailToAnonymousUser(string url)
        {
            var provider = TestClaimsProvider.WithUserClaims();
            var client = Factory.CreateClientWithTestAuth(provider);

            var response = await client.GetAsync(url);

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Theory]
        [InlineData("/Cars/Edit/")]
        [InlineData("/Cars/Edit/1")]
        public async Task Get_EditReturnsFailToRegularUser(string url)
        {
            var provider = TestClaimsProvider.WithUserClaims();
            var client = Factory.CreateClientWithTestAuth(provider);

            var response = await client.GetAsync(url);

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Theory]
        [InlineData("/Cars/Edit/1")]
        public async Task Get_EditReturnsSuccessToAdminUser(string url)
        {
            var provider = TestClaimsProvider.WithAdminClaims();
            var client = Factory.CreateClientWithTestAuth(provider);

            var response = await client.GetAsync(url);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/Cars/Details/1")]
        public async Task Get_DetailsReturnsSuccessToRegularUser(string url)
        {
            var provider = TestClaimsProvider.WithUserClaims();
            var client = Factory.CreateClientWithTestAuth(provider);

            var response = await client.GetAsync(url);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/Cars/Details/1")]
        public async Task Get_DetailsReturnsSuccessToAdminUser(string url)
        {
            var provider = TestClaimsProvider.WithAdminClaims();
            var client = Factory.CreateClientWithTestAuth(provider);

            var response = await client.GetAsync(url);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/Cars/Delete/")]
        [InlineData("/Cars/Delete/1")]
        public async Task Get_DeleteReturnsFailToAnonymousUser(string url)
        {
            var provider = TestClaimsProvider.WithUserClaims();
            var client = Factory.CreateClientWithTestAuth(provider);

            var response = await client.GetAsync(url);

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Theory]
        [InlineData("/Cars/Delete/")]
        [InlineData("/Cars/Delete/1")]
        public async Task Get_DeleteReturnsFailToRegularUser(string url)
        {
            var provider = TestClaimsProvider.WithUserClaims();
            var client = Factory.CreateClientWithTestAuth(provider);

            var response = await client.GetAsync(url);

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Theory]
        [InlineData("/Cars/Delete/1")]
        public async Task Get_DeleteReturnsSuccessToAdminUser(string url)
        {
            var provider = TestClaimsProvider.WithAdminClaims();
            var client = Factory.CreateClientWithTestAuth(provider);

            var response = await client.GetAsync(url);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }



        [Fact]
        public async Task Edit_EndpointRetrunSuccessForCorrectModel()
        {
            var claimsProvider = TestClaimsProvider.WithAdminClaims();
            var client = Factory.CreateClientWithTestAuth(claimsProvider);

            var formValues = new Dictionary<string, string>();

            formValues.Add("Id", "1");
            formValues.Add("LicencePlate", "898");
            formValues.Add("KmFare", "63.0");
            formValues.Add("TimeFare", "0.0");
            formValues.Add("CarModelId", "1");



            var content = new FormUrlEncodedContent(formValues);
            var response = await client.PostAsync("/Cars/Edit", content);

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);

        }

        [Fact]
        public async Task Create_EndpointRetrunSuccessForCorrectModel()
        {
            var claimsProvider = TestClaimsProvider.WithAdminClaims();
            var client = Factory.CreateClientWithTestAuth(claimsProvider);

            var formValues = new Dictionary<string, string>();

            formValues.Add("Id", "1");
            formValues.Add("LicencePlate", "898");
            formValues.Add("KmFare", "63.0");
            formValues.Add("TimeFare", "0.0");
            formValues.Add("CarModelId", "1");



            var content = new FormUrlEncodedContent(formValues);
            var response = await client.PostAsync("/Cars/Create", content);

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);

        }
    }
}
