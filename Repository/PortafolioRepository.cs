using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class PortafolioRepository : IPortafolioRepository
    {
        private readonly ApplicationDBContext _context;
        public PortafolioRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Portafolio> BorrarPortafolio(User user, string symbol)
        {
            var portafolioModel = await _context.Portafolios.FirstOrDefaultAsync(e => e.IdUsuario == user.Id && e.Stock.Symbol.ToLower() == symbol.ToLower());
            if (portafolioModel == null)
            {
                return null;
            };
            
            _context.Portafolios.Remove(portafolioModel);
            await _context.SaveChangesAsync();

            return portafolioModel;
        }

        public async Task<Portafolio> CrearPortafolioAsync(Portafolio portafolio)
        {
            await _context.Portafolios.AddAsync(portafolio);
            await _context.SaveChangesAsync();
            return portafolio;
        }

        public async Task<List<Stock>> GetUsuarioPortafolio(User user)
        {
            return await _context.Portafolios.Where(x => x.IdUsuario == user.Id).Select(stock => new Stock
            {
                Id = stock.IdStock,
                Symbol = stock.Stock.Symbol,
                NombreCompania = stock.Stock.NombreCompania,
                Compra = stock.Stock.Compra,
                UltimaVenta = stock.Stock.UltimaVenta,
                Industria = stock.Stock.Industria,
                MarketCap = stock.Stock.MarketCap
            }).ToListAsync();
        }
    }
}