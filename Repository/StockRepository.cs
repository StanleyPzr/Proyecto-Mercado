using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos.Stock;
using API.Helpers;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;             
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(X => X.Id == id);
            if (stockModel == null)
            {
                return null;
            }
            _context.Stocks.Remove(stockModel); 
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stocks =  _context.Stocks.Include(c=>c.Comentarios).AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.CompanyName)){
                stocks = stocks.Where(s => s.NombreCompania.Contains(query.CompanyName));
            }

            if(!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }

            if(!string.IsNullOrWhiteSpace(query.SortBy)){
                if(query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDecsending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
            }

            var saltarNumero = (query.NumPag - 1) * query.TamPag;


            return await stocks.Skip(saltarNumero).Take(query.TamPag).ToListAsync();


        }
        
        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c=>c.Comentarios).FirstOrDefaultAsync(i => i.Id==id);
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return  await _context.Stocks.FirstOrDefaultAsync(c=>c.Symbol==symbol);
        }

        public Task<bool> StockExists(int id)
        {
            return _context.Stocks.AnyAsync(x => x.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDTO stockDto)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(X => X.Id == id);
            if (existingStock == null){
                return null;
            }
            existingStock.Symbol = stockDto.Symbol;
            existingStock.NombreCompania = stockDto.NombreCompania;
            existingStock.Compra = stockDto.Compra;
            existingStock.UltimaVenta = stockDto.UltimaVenta;
            existingStock.Industria = stockDto.Industria;
            existingStock.MarketCap = stockDto.MarketCap;
            await _context.SaveChangesAsync();

            return existingStock;
        }
    }
}