using Podcast.Models;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Podcast.DAL
{
    public class PodcastContext : DbContext
    {
        public PodcastContext()
            : base("PodcastContext")
        {
            Database.SetInitializer<PodcastContext>(new MigrateDatabaseToLatestVersion<PodcastContext, Configuration>());

        }

        public DbSet<Episodio> Episodios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}