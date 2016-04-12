using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Podcast.Models
{
    public class PodcastBase
    {
        public int PodcastBaseID { get; set; }
        public string dsTitulo { get; set; }
        public string dsPodcast { get; set; }
        public string dtGravacao { get; set; }
        public int nrEdicao { get; set; }
        public string nmArquivoAudio { get; set; }
        public string nmArquivoImagem { get; set; }
    }
}