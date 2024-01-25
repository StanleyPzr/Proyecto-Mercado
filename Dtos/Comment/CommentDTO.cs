using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Comment
{
    //Las validaciones van directamente al DTO antes que al modelo, evitar validar Modelos
    public class CommentDTO
    {
        public int Id {get; set;}
        public string Titulo {get; set;} = string.Empty;
        public string Contenido {get; set;} = string.Empty;
        public DateTime CreadoEn {get; set;} = DateTime.Now;
        public int? StockId { get; set; }
        
    }
}