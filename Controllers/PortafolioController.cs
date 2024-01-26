using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/portafolio")]
    public class PortafolioController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortafolioRepository _portafolioRepository;
        public PortafolioController(UserManager<User> userManager, IStockRepository stockRepository, IPortafolioRepository portafolioRepository)
        {
            _stockRepository = stockRepository;
            _userManager = userManager;
            _portafolioRepository = portafolioRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUsuarioPortafolio()
        {
            var usuario = User.GetNombreUsuario();
            var user = await _userManager.FindByNameAsync(usuario);
            var portafolio = await _portafolioRepository.GetUsuarioPortafolio(user);

            return Ok(portafolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AgregarPortafolio(String symbol)
        {
            var usuario = User.GetNombreUsuario();
            var user = await _userManager.FindByNameAsync(usuario);
            var stock = await _stockRepository.GetBySymbolAsync(symbol);
            if (stock == null) return BadRequest("Stock no encontrado");



            var portafolio = await _portafolioRepository.GetUsuarioPortafolio(user);

            if(portafolio.Any(e => e.Symbol.ToLower() == symbol.ToLower())) return BadRequest("No puedes añadir el mismo stock al portafolio");

            var portafolioModel = new Portafolio
            {
                IdStock = stock.Id,
                IdUsuario = user.Id
            };
            
            await _portafolioRepository.CrearPortafolioAsync(portafolioModel);

            if(portafolioModel == null) 
            {
                return StatusCode(500, "No se pudo añadir el stock al portafolio");
            }
            else
            {
                return StatusCode(201, "Portafolio agregado correctamente");
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> BorrarPortafolio(String symbol)
        {
            var usuario = User.GetNombreUsuario();
            var user = await _userManager.FindByNameAsync(usuario);
            var portafolio = await _portafolioRepository.GetUsuarioPortafolio(user);

            var filtrarStock = portafolio.Where(x => x.Symbol.ToLower() == symbol.ToLower()).ToList();
            if(filtrarStock.Count == 1)
            {
                await _portafolioRepository.BorrarPortafolio(user, symbol);
            }
            else
            {
                return BadRequest("Stock no encontrado en tu portafolio");
            }

            return Ok("Portafolio eliminado correctamente");
        }
    }
}