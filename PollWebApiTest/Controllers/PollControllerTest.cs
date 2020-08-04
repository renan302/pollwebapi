using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PollWebApi;
using PollWebApi.Request;
using PollWebApiTest.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PollWebApiTest
{
    public class PollControllerTest
    {
        private readonly HttpClient _client;
        private PollControllerMock mock;

        public StringContent ObjectToJson(Object param)
        {
            var json = JsonConvert.SerializeObject(param);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            return data;
        }

        public PollControllerTest()
        {
            
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")                
                .UseConfiguration(new ConfigurationBuilder()                    
                    .AddJsonFile("appsettings.json")
                    .Build()
                )
                .UseStartup<Startup>());

            _client = server.CreateClient();

            mock = new PollControllerMock();
        }

        [Theory]
        [InlineData("POST")]
        public async Task TestPost(string method)
        {           
            mock._testPost.ToList().ForEach(async pollPostRequest => {

                var response = await _client.PostAsync("/poll", this.ObjectToJson(pollPostRequest));

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            });            
        }

        [Theory]
        [InlineData("POST")]
        public async Task TestPostBadRequest(string method)
        {
            mock._testPostBadRequest.ToList().ForEach(async pollPostRequest =>
            {
                var response = await _client.PostAsync("/poll", null);

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            });
        }       

        [Theory]
        [InlineData("GET")]
        public async Task TestGet(string method)
        {
            mock._testGet.ToList().ForEach(async poll =>
            {

                var request = new HttpRequestMessage(new HttpMethod(method), "/poll/" + poll);

                var response = await _client.SendAsync(request);

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            });
        }

        [Theory]
        [InlineData("GET")]
        public async Task TestGetBadRequest(string method)
        {
            mock._testGetBadRequest.ToList().ForEach(async poll =>
            {
                var request = new HttpRequestMessage(new HttpMethod(method), "/poll/" + poll);

                var response = await _client.SendAsync(request);

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            });
        }

        [Theory]
        [InlineData("GET")]
        public async Task TestGetNotFound(string method)
        {
            mock._testGetNotFound.ToList().ForEach(async poll =>
            {
                var request = new HttpRequestMessage(new HttpMethod(method), "/poll/"+ poll);

                var response = await _client.SendAsync(request);

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            });
        }

        [Theory]
        [InlineData("POST")]
        public async Task TestPostVote(string method)
        {
            mock._testPostVote.ToList().ForEach(async voteRequest =>
            {               

                var response = await _client.PostAsync("/poll/1/vote", this.ObjectToJson(voteRequest));

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            });
        }

        [Theory]
        [InlineData("POST")]
        public async Task TestPostVoteBadRequest(string method)
        {
            mock._testPostVoteBadRequest.ToList().ForEach(async voteRequest =>
            {
                var response = await _client.PostAsync("/poll/XXX/vote", this.ObjectToJson(voteRequest));

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            });
        }

        [Theory]
        [InlineData("POST")]
        public async Task TestPostVoteNotFound(string method)
        {
            mock._testPostVoteNotFound.ToList().ForEach(async voteRequest =>
            {
                var response = await _client.PostAsync("/poll/5555/vote", this.ObjectToJson(voteRequest));

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            });
        }



        [Theory]
        [InlineData("GET")]
        public async Task TestGetStats(string method)
        {
            mock._testGetStats.ToList().ForEach(async poll =>
            {
                var request = new HttpRequestMessage(new HttpMethod(method), "/poll/"+ poll + "/stats");

                var response = await _client.SendAsync(request);

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            });
        }

        [Theory]
        [InlineData("GET")]
        public async Task TestGetStatsBadRequest(string method)
        {
            mock._testGetStatsBadRequest.ToList().ForEach(async poll =>
            {
                var request = new HttpRequestMessage(new HttpMethod(method), "/poll/"+ poll + "/stats");

                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            });
        }

        [Theory]
        [InlineData("GET")]
        public async Task TestGetStatsNotFound(string method)
        {
            mock._testGetStatsNotFound.ToList().ForEach(async poll =>
            {
                var request = new HttpRequestMessage(new HttpMethod(method), "/poll/"+ poll + "/stats");

                var response = await _client.SendAsync(request);

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            });
        }
    }
}
