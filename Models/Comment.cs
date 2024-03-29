using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    //Evitar validaciones acá
    [Table("Comentarios") ]
    public class Comment
    {
        public int Id {get; set;}
        public string Titulo {get; set;} = string.Empty;
        public string Contenido {get; set;} = string.Empty;
        public DateTime CreadoEn {get; set;} = DateTime.Now;
        public int? StockId { get; set; } //Propiedad de navegación
        public Stock? Stock {get; set;}
    }
}