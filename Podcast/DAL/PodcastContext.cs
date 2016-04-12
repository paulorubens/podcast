using Podcast.Models;
using System.Data.Entity;

namespace Podcast.DAL
{
    public class PodcastContext : DbContext
    {
        public PodcastContext()
            : base("PodcastContext")
        {
        }

        public DbSet<PodcastBase> Podcasts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}