using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Podcast.Models
{
    public class Podcast
    {
        [Key]
        public int PodcastID { get; set; }

        public string NmPodcast { get; set; }
        public string DsPodcast { get; set; }

        public ICollection<Episodio> Episodios { get; set; }
    }
}