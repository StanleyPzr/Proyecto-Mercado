using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Stock
{
    public class CreateStockRequestDTO
    {
        public string Symbol {get; set;} = string.Empty;
        public string NombreCompania{get; set;} = string.Empty;       
        public decimal Compra {get; set;}        
        public decimal UltimaVenta {get; set;}
        public string Industria {get; set;} = string.Empty;
        public long MarketCap {get; set;}
    }
}