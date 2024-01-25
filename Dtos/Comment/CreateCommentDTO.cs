using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Comment
{
    public class CreateCommentDTO
    {
        public string Titulo {get; set;} = string.Empty;
        public string Contenido {get; set;} = string.Empty;
    }
}