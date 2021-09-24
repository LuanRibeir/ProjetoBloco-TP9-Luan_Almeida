using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamedevNetwork.Domain.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public string Texto { get; set; }
        [DisplayName("Data de Criação")]
        public DateTime DataCriacao { get; set; }
        public GamedevProfile GamedevProfile { get; set; }
    }
}
