using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PollWebApi.Models
{
    [Table("vote")]
    public class VoteClass
    {
        [Key]
        public Int64 vote_id { get; set; }
        [Required]
        [ForeignKey("Option")]
        public Int64 option_id { get; set; }
        public OptionClass Option { get; set; }
    }
}
