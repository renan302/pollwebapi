using PollWebApi.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace PollWebApiTest.Mock
{
    public class PollControllerMock
    {        
        public IEnumerable<PollPostRequest> _testPost {get;set;}
        public IEnumerable<PollPostRequest> _testPostBadRequest { get; set; }

        public IEnumerable<string> _testGet { get; set; }
        public IEnumerable<string> _testGetExpected { get; set; }
        public IEnumerable<string> _testGetBadRequest { get; set; }
        public IEnumerable<string> _testGetNotFound { get; set; }

        public IEnumerable<VotePostRequest> _testPostVote { get; set; }
        public IEnumerable<VotePostRequest> _testPostVoteNotFound { get; set; }
        public IEnumerable<VotePostRequest> _testPostVoteBadRequest { get; set; }

        public IEnumerable<string> _testGetStats { get; set; }
        public IEnumerable<string> _testGetStatsNotFound { get; set; }
        public IEnumerable<string> _testGetStatsBadRequest { get; set; } 

        public PollControllerMock()
        {
            _testPost = new List<PollPostRequest>();
            _testPostBadRequest = new List<PollPostRequest>();

            _testGet = new List<string>();
            _testGetExpected = new List<string>();
            _testGetBadRequest = new List<string>();
            _testGetNotFound = new List<string>();

            _testPostVote = new List<VotePostRequest>() { };
            _testPostVoteNotFound = new List<VotePostRequest>();
            _testPostVoteBadRequest = new List<VotePostRequest>();

            _testGetStats = new List<string>();
            _testGetStatsNotFound = new List<string>();
            _testGetStatsBadRequest = new List<string>();
        }
    }
}
