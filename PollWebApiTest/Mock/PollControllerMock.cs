using PollWebApi.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace PollWebApiTest.Mock
{
    public class PollControllerMock
    {        
        public ICollection<PollPostRequest> _testPost {get;set;}
        public ICollection<Object> _testPostBadRequest { get; set; }

        public ICollection<string> _testGet { get; set; }        
        public ICollection<string> _testGetBadRequest { get; set; }
        public ICollection<string> _testGetNotFound { get; set; }

        public ICollection<VotePostRequest> _testPostVoteBody { get; set; }
        public ICollection<string> _testPostVoteParams { get; set; }
        public ICollection<string> _testPostVoteParamsNotFound { get; set; }
        public ICollection<VotePostRequest> _testPostVoteBodyNotFound { get; set; }
        public ICollection<string> _testPostVoteParamsBadRequest { get; set; }
        public ICollection<Object> _testPostVoteBodyBadRequest { get; set; }

        public ICollection<string> _testGetStats { get; set; }
        public ICollection<string> _testGetStatsNotFound { get; set; }
        public ICollection<string> _testGetStatsBadRequest { get; set; }

        private void createTestPost()
        {
            _testPost = new List<PollPostRequest>();

            _testPost.Add(new PollPostRequest
            {
                poll_description = "Teste Enquete",
                options = new List<string> { "Opção 1", "Opção 2", "Opção 2" }
            });

            _testPost.Add(new PollPostRequest
            {
                poll_description = "Teste Enquete",
                options = new List<string> { "Opção 1", "Opção 2", "Opção 2" }
            });

            _testPost.Add(new PollPostRequest
            {
                poll_description = "Teste Enquete",
                options = new List<string> { "Opção 1", "Opção 2", "Opção 2" }
            });

            _testPostBadRequest = new List<Object>();

            _testPostBadRequest.Add(new 
            {                
                options = new List<string> { "Opção 1", "Opção 2", "Opção 2" }
            });

            _testPostBadRequest.Add(new 
            {
                options = new List<string> { "Opção 1", "Opção 2", "Opção 2" }
            });
        }

        private void createTestGet()
        {
            _testGet = new List<string>() { "1", "2" };                  
            
            _testGetBadRequest = new List<string>() { "xxxxxx", "212312312312312312131213" };

            _testGetNotFound = new List<string>() {"-100", "10000"};

        }        

        private void createTestPostVote()
        {                                
            _testPostVoteParams = new List<string> { "1","1", "1", "1" };

            _testPostVoteBody = new List<VotePostRequest>();
            _testPostVoteBody.Add(new VotePostRequest
            {
                option_id = 1,
            });
            _testPostVoteBody.Add(new VotePostRequest
            {
                option_id = 1,
            });
            _testPostVoteBody.Add(new VotePostRequest
            {
                option_id = 1,
            });
            _testPostVoteBody.Add(new VotePostRequest
            {
                option_id = 2,
            });


            _testPostVoteParamsNotFound = new List<string>() {"-50", "100000", "1"};

            _testPostVoteBodyNotFound = new List<VotePostRequest>();
            _testPostVoteBodyNotFound.Add(new VotePostRequest
            {
                option_id = 2222233,
            });
            _testPostVoteBodyNotFound.Add(new VotePostRequest
            {
                option_id = 2222233,
            });
            _testPostVoteBodyNotFound.Add(new VotePostRequest
            {
                option_id = 2222233,
            });


            _testPostVoteParamsBadRequest = new List<string>() { "xxxxx", "15151561651561561516", "1" };

            _testPostVoteBodyBadRequest = new List<Object>();
            _testPostVoteBodyBadRequest.Add(new 
            {
                option_id = 2,
            });
            _testPostVoteBodyBadRequest.Add(new 
            {
                option_id = 2,
            });
            _testPostVoteBodyBadRequest.Add(new 
            {
                option_id = "4747474222222232327",
            });

        }
        private void createTestGetStats()
        {
            _testGetStats = new List<string>() { "1", "2", "3"};

            _testGetStatsNotFound = new List<string>() { "54545646" };

            _testGetStatsBadRequest = new List<string>() { "xxxw", "564654564654564654644" };

        }

        public PollControllerMock()
        {
            this.createTestGet();

            this.createTestGetStats();

            this.createTestPost();

            this.createTestPostVote();                    

        }


    }
}
