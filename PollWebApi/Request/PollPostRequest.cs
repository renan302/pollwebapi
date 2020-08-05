using PollWebApi.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PollWebApi.Request
{
    public class PollPostRequest
    {
        [Required]
        public string poll_description { get; set; }
        [Required]
        public IEnumerable<string> options { get; set; }

        public static PollClass ToPollClass(PollPostRequest pollPostRequest)
        {

            PollClass pollClass = new PollClass() { poll_description = pollPostRequest.poll_description };

            foreach (var option in pollPostRequest.options)
            {
                pollClass.options.Add(new OptionClass() { option_description = option });
            }

            return pollClass;
        }
    }
}
