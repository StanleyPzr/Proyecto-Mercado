using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    //Evitar validaciones acá
    public class Stock
    {
        public int Id {get; set;}
        public string Symbol {get; set;} = string.Empty;
        public string NombreCompania{get; set;} = string.Empty;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Compra {get; set;}
        [Column(TypeName = "decimal(18, 2)")]
        public decimal UltimaVenta {get; set;}
        public string Industria {get; set;} = string.Empty;
        public long MarketCap {get; set;}

        public List<Comment> Comentarios {get; set;} = new List<Comment>();
    }
}