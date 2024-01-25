using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Stock
{
    public class UpdateStockRequestDTO
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Las siglas no deben superar los 10 caracteres")]
        public string Symbol {get; set;} = string.Empty;

        [Required]
        [MaxLength(10, ErrorMessage = "El nombre de la compañia no debe superar los 10 caracteres")]
        public string NombreCompania{get; set;} = string.Empty;

        [Required]
        [Range(1,1000000000)]      
        public decimal Compra {get; set;} 

        [Required]
        [Range(0.001,100)]        
        public decimal UltimaVenta {get; set;}

        [Required]
        [MaxLength(10, ErrorMessage = "El nombre de la Industria no debe superar los 10 caracteres")]
        public string Industria {get; set;} = string.Empty;

        [Range(1,5000000000)]
        public long MarketCap {get; set;}
    }
}