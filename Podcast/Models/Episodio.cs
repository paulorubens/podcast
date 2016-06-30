using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Podcast.Models
{
    public class Episodio
    {
        [Key]
        public int EpisodioID { get; set; }

        public string dsTitulo { get; set; }
        public string dsPodcast { get; set; }
        public DateTime dtGravacao { get; set; }
        public int nrEdicao { get; set; }
        public string nmArquivoAudio { get; set; }
        public string nmArquivoImagem { get; set; }

        public Nullable<int> PodcastID { get; set; }

        [ForeignKey("PodcastID")]
        public Podcast Podcast { get; set; }
    }
}