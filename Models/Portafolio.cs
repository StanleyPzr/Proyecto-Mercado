using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Portafolios")]
    public class Portafolio
    {
        public string IdUsuario { get; set; }
        public int IdStock { get; set; }
        public User User { get; set; }
        public Stock Stock { get; set; }
    }
}