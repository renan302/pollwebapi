using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using PollWebApi;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace PollWebApiTest
{
    public class PollControllerTest
    {
        private readonly HttpClient _client;

        public PollControllerTest()
        {            
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());

            _client = server.CreateClient();
        }

        [Theory]
        [InlineData("GET")]
        public async Task TestGet(string method)
        {
            var request = new HttpRequestMessage(new HttpMethod(method), "/poll/1/stats");

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("POST")]
        public async Task TestPost(string method)
        {
            var request = new HttpRequestMessage(new HttpMethod(method), "/poll");

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("GET")]
        public async Task TestGetStats(string method)
        {
            var request = new HttpRequestMessage(new HttpMethod(method), "/poll/1/stats");

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
