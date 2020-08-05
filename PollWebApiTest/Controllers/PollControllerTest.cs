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
            foreach (var pollPostRequest in mock._testPost)
            {

                var response = await _client.PostAsync("/poll", this.ObjectToJson(pollPostRequest));

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            };            
        }

        [Theory]
        [InlineData("POST")]
        public async Task TestPostBadRequest(string method)
        {
            foreach (var pollPostRequest in mock._testPostBadRequest)
            {
                var response = await _client.PostAsync("/poll", this.ObjectToJson(pollPostRequest));

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            };
        }       

        [Theory]
        [InlineData("GET")]
        public async Task TestGet(string method)
        {
            foreach(var poll in mock._testGet)
            {

                var request = new HttpRequestMessage(new HttpMethod(method), "/poll/" + poll);

                var response = await _client.SendAsync(request);

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            };
        }

        [Theory]
        [InlineData("GET")]
        public async Task TestGetBadRequest(string method)
        {            
            foreach (var poll in mock._testGetBadRequest)
            {
                var request = new HttpRequestMessage(new HttpMethod(method), "/poll/" + poll);

                var response = await _client.SendAsync(request);

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            };
        }

        [Theory]
        [InlineData("GET")]
        public async Task TestGetNotFound(string method)
        {
            foreach (var poll in mock._testGetNotFound)
            {
                var request = new HttpRequestMessage(new HttpMethod(method), "/poll/"+ poll);

                var response = await _client.SendAsync(request);

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            };
        }

        [Theory]
        [InlineData("POST")]
        public async Task TestPostVote(string method)
        {
            int i = 0;
            foreach (var voteRequest in mock._testPostVoteParams)
            {

                var response = await _client.PostAsync("/poll/"+voteRequest+"/vote", this.ObjectToJson(mock._testPostVoteBody.ElementAt(i)));

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                i++;
            };
        }

        [Theory]
        [InlineData("POST")]
        public async Task TestPostVoteBadRequest(string method)
        {
            int i = 0;
            foreach (var voteRequest in mock._testPostVoteParamsBadRequest)
            {
                var response = await _client.PostAsync("/poll/"+ voteRequest + "/vote", this.ObjectToJson(mock._testPostVoteBodyBadRequest.ElementAt(i)));

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                i++;
            };
        }

        [Theory]
        [InlineData("POST")]
        public async Task TestPostVoteNotFound(string method)
        {
            int i = 0;
            foreach (var voteRequest in mock._testPostVoteParamsNotFound)
            {
                var response = await _client.PostAsync("/poll/"+ voteRequest + "/vote", this.ObjectToJson(mock._testPostVoteBodyNotFound.ElementAt(i)));

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

                i++;
            };
        }



        [Theory]
        [InlineData("GET")]
        public async Task TestGetStats(string method)
        {       
            foreach (var poll in mock._testGetStats)
            {
                var request = new HttpRequestMessage(new HttpMethod(method), "/poll/"+ poll + "/stats");

                var response = await _client.SendAsync(request);

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            };
        }

        [Theory]
        [InlineData("GET")]
        public async Task TestGetStatsBadRequest(string method)
        {
            foreach (var poll in mock._testGetStatsBadRequest)
            {
                var request = new HttpRequestMessage(new HttpMethod(method), "/poll/"+ poll + "/stats");

                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            };
        }

        [Theory]
        [InlineData("GET")]
        public async Task TestGetStatsNotFound(string method)
        {
            foreach (var poll in mock._testGetStatsNotFound)
            {
                var request = new HttpRequestMessage(new HttpMethod(method), "/poll/"+ poll + "/stats");

                var response = await _client.SendAsync(request);

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            };
        }
    }
}
