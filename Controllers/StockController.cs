using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos.Stock;
using API.Interfaces;
using API.Mappers;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/stock")]
    [ApiController]   
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepository;
        public StockController(ApplicationDBContext context, IStockRepository stockRepository)
        {
            _context = context;
            _stockRepository = stockRepository;          
        }   

        [HttpGet]
        [Authorize]        
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var stocks = await _stockRepository.GetAllAsync(query);
            var stockDto = stocks.Select(s => s.ToStockDTO());            
            return Ok(stocks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CreateStockRequestDTO stockDTO)
        {
            var stockModel = stockDTO.ToStockFromCreateDTO();
            await _stockRepository.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDTO());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar([FromRoute] int id, [FromBody] UpdateStockRequestDTO updateDTO)
        {
            var stockModel = await _stockRepository.UpdateAsync(id, updateDTO);
            if (stockModel == null)
            {
                return NotFound();
            }
           
            return Ok(stockModel.ToStockDTO());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            var stockModel = await _stockRepository.DeleteAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }
            _context.Stocks.Remove(stockModel);
          
            return Ok("Stock eliminado correctamente");
        }
    }
}