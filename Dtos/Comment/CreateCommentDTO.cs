using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Comment
{
    public class CreateCommentDTO
    {
        [Required]
        [MinLength(10, ErrorMessage = "El titulo debe tener al menos 10 caracteres")]
        [MaxLength(280, ErrorMessage = "El titulo debe tener un maximo de 280 caracteres")]
        public string Titulo {get; set;} = string.Empty;

        [Required]
        [MinLength(10, ErrorMessage = "El contenido debe tener al menos 10 caracteres")]
        [MaxLength(280, ErrorMessage = "El contenido debe tener un maximo de 280 caracteres")]
        public string Contenido {get; set;} = string.Empty;
    }
}