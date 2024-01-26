using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Account
{
    public class LoginDTO
    {
        [Required]
        public string Usuario {get; set;}

        [Required]
        public string Password {get; set;}
    }
}