using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamedevNetwork.Mvc.Models
{
    public class GamedevProfile
    {
        public int Id { get; set; }
        public string ImagemUri { get; set; }
        public string Nome { get; set; }
        public string Nacionalidade { get; set; }
        public string JogoFavorito { get; set; }
        public int QuantidadeDeJogosPublicados { get; set; }
        public DateTime Nascimento { get; set; }
        public ICollection<Post> Posts { get; set; }

        public string UserId { get; set; }
    }
}
