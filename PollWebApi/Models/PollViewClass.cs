using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PollWebApi.Models
{
    [Table("pollview")]
    public class PollViewClass
    {
        [Key]
        public Int64 pollview_id { get; set; }
        [ForeignKey("Poll")]
        public Int64 poll_id { get; set; }
        public PollClass Poll { get; set; }
    }
}
