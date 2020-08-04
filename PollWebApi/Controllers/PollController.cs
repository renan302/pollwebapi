using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using PollWebApi.Context;
using PollWebApi.Models;
using PollWebApi.Request;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PollWebApi.Controllers
{
    [Route("poll")]
    [ApiController]
    public class PollController : ControllerBase
    {

        private readonly PollContext _context;

        public PollController(PollContext context)
        {
            this._context = context;
        }

        // GET: api/poll/v1/5
        [HttpGet("{id}")]
        public ActionResult<Object> Get(int id, [FromServices]IConfiguration config,
            [FromServices]IDistributedCache cache)
        {
            Object pollClass;            
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };

                pollClass = cache.GetString("get.poll."+id.ToString());                               

                if (pollClass == null)
                {
                    pollClass = PollClass.findObject(_context, id);

                    DistributedCacheEntryOptions opcoesCache =
                    new DistributedCacheEntryOptions();

                    opcoesCache.SetAbsoluteExpiration(
                    TimeSpan.FromMinutes(15));                    

                    cache.SetString("get.poll." + id.ToString(), 
                           JsonSerializer.Serialize(pollClass, options), opcoesCache);
                }

            }
            catch (KeyNotFoundException e)
            {
                return NotFound();
            }

            return Ok(pollClass);
        }

        // POST: api/poll/v1
        [HttpPost]
        public IActionResult Post([FromBody] PollPostRequest pollRequest)
        {

            PollClass pollClass;

            try
            {
                pollClass = PollClass.add(_context, PollPostRequest.ToPollClass(pollRequest));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound();
            }


            return Ok(new { poll_id = pollClass.poll_id });

        }

        // POST: api/poll/v1/{id}/vote
        [HttpPost("{id}/vote")]
        public IActionResult Vote(int id, [FromBody] VotePostRequest vote)
        {

            try
            {
                OptionClass.RegisterVote(_context, id, vote.option_id);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound();
            }

            return Ok();

        }

        // GET: api/poll/v1/5
        [HttpGet("{id}/stats")]
        public IActionResult Stats(int id)
        {
            
            Object statsClass;

            try
            {
                statsClass = PollClass.findStats(_context, id);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound();
            }

            return Ok(statsClass);

        }

    }
}
