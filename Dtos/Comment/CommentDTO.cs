using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Comment
{
    public class CommentDTO
    {
        public int Id {get; set;}
        public string Titulo {get; set;} = string.Empty;
        public string Contenido {get; set;} = string.Empty;
        public DateTime CreadoEn {get; set;} = DateTime.Now;
        public int? StockId { get; set; }
        
    }
}