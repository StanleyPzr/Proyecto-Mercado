using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.Interfaces
{
    public interface IPortafolioRepository
    {
        Task<List<Stock>> GetUsuarioPortafolio(User user);

        Task<Portafolio> CrearPortafolioAsync(Portafolio portafolio);

        Task<Portafolio> BorrarPortafolio(User user, string symbol);
    }
}