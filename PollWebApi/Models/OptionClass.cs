using PollWebApi.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PollWebApi.Models
{
    [Table("option")]
    public class OptionClass
    {
        [Key]
        public Int64 option_id { get; set; }

        [Required]
        [ForeignKey("Poll")]
        public Int64 poll_id { get; set; }

        [Required]
        public string option_description { get; set; }
        public PollClass Poll { get; set; }

        public static void RegisterVote(PollContext context, Int64 poll_id, Int64 option_id)
        {
            var pollClass = PollClass.find(context, poll_id);

            var option = pollClass.options.FirstOrDefault(x => x.option_id == option_id);

            if (option == null)
            {
                throw new KeyNotFoundException();
            }

            VoteClass vote = new VoteClass { option_id = option_id };

            context.Vote.Add(vote);
            context.SaveChanges();
        }

        public int getVotes(PollContext context)
        {
            return context.Vote.Count(vote => vote.option_id == this.option_id);
        }
    }
}
