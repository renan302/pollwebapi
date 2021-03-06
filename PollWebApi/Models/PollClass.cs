﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using PollWebApi.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace PollWebApi.Models
{
    [Table("poll")]
    public class PollClass
    {
        [Key]
        public Int64 poll_id { get; set; }

        [Required]
        public string poll_description { get; set; }

        public ICollection<OptionClass> options { get; set; }

        public PollClass()
        {
            options = new List<OptionClass>();
        }

        public static PollClass add(PollContext context, PollClass pollclass)
        {
            context.Poll.Add(pollclass);
            context.SaveChanges();

            return pollclass;
        }

        public static PollClass find(PollContext context, Int64 id)
        {

            
            PollClass pollClass = context.Poll.Include(x => x.options).FirstOrDefault(x => x.poll_id == id);

            if (pollClass == null)
            {
                throw new KeyNotFoundException();
            }

            return pollClass;
        }

        public static Object findObject(PollContext context, IDistributedCache cache, Int64 id)
        {
            Object pollClass;

            pollClass = cache.GetString("find.poll." + id.ToString());

            if (pollClass == null)
            {

                pollClass = context.Poll
                .Include(x => x.options)
                .Select(x => new { x.poll_id, x.poll_description, options = x.options.Select(z => new { z.option_id, z.option_description }) })
                .FirstOrDefault(x => x.poll_id == id);

                if (pollClass == null)
                {
                    throw new KeyNotFoundException();
                }

                DistributedCacheEntryOptions opcoesCache =
                    new DistributedCacheEntryOptions();

                opcoesCache.SetAbsoluteExpiration(
                TimeSpan.FromMinutes(15));

                string output = JsonConvert.SerializeObject(pollClass);

                cache.SetString("find.poll." + id.ToString(), output, opcoesCache);
            }
            else
            {
                pollClass = JsonConvert.DeserializeObject<Object>(pollClass.ToString());
            }

            PollClass.registerView(context, id);

            return pollClass;
        }

        public static void registerView(PollContext context, Int64 id)
        {
            PollViewClass pollview = new PollViewClass { poll_id = id };

            context.PollView.Add(pollview);
            context.SaveChanges();
        }

        public static Object findStats(PollContext context, Int64 id)
        {

            var pollClass = PollClass.find(context, id);

            if (pollClass == null)
            {
                throw new KeyNotFoundException();
            }

            var stats = new
            {
                views = PollClass.getViews(context, id),
                votes = pollClass.options.Select(p =>
                    new { option_id = p.option_id, qty = p.getVotes(context) }).ToList()
            };

            return stats;

        }

        private static int getViews(PollContext context, Int64 poll_id)
        {
            return context.PollView.Count(pollview => pollview.poll_id == poll_id);
        }
    }
}
