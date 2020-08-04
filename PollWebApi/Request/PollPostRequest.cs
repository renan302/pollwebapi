using PollWebApi.Models;
using System.Collections.Generic;

namespace PollWebApi.Request
{
    public class PollPostRequest
    {
        public string poll_description { get; set; }
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
