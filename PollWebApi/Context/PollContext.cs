using Microsoft.EntityFrameworkCore;
using PollWebApi.Models;

namespace PollWebApi.Context
{
    public class PollContext : DbContext
    {
        public DbSet<PollClass> Poll { get; set; }

        public DbSet<VoteClass> Vote { get; set; }

        public DbSet<PollViewClass> PollView { get; set; }

        public DbSet<OptionClass> Option { get; set; }

        public PollContext(DbContextOptions<PollContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
