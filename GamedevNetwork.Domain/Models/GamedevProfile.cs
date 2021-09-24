using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamedevNetwork.Domain.Models
{
    public class GamedevProfile
    {
        public int Id { get; set; }
        [DisplayName("Foto")]
        public string ImagemUri { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Nacionalidade { get; set; }
        [Required]
        [DisplayName("Jogo Favorito")]
        public string JogoFavorito { get; set; }
        [DisplayName("Total de Jogos")]
        public int QuantidadeDeJogosPublicados { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Nascimento { get; set; }
        public ICollection<Post> Posts { get; set; }

        public string UserId { get; set; }
    }
}
