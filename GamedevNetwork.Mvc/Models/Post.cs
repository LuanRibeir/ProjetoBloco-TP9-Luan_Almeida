using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamedevNetwork.Mvc.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public DateTime DataCriacao { get; set; }
        public GamedevProfile GamedevProfile { get; set; }
    }
}
