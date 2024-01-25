using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Stock;
using API.Models;

namespace API.Mappers
{
    public static class StockMapper
    {
        public static StockDTO ToStockDTO(this Stock stockModel){
            return new StockDTO
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                NombreCompania = stockModel.NombreCompania,
                Compra = stockModel.Compra,
                UltimaVenta = stockModel.UltimaVenta,
                Industria = stockModel.Industria,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comentarios.Select(c => c.ToCommentDTO()).ToList()
            };
        }

        public static Stock ToStockFromCreateDTO(this CreateStockRequestDTO stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                NombreCompania = stockDto.NombreCompania,
                Compra = stockDto.Compra,
                UltimaVenta = stockDto.UltimaVenta,
                Industria = stockDto.Industria,
                MarketCap = stockDto.MarketCap
            };
        }
    }
}