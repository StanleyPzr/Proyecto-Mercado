using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Account;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager) 
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = await _userManager.Users.FirstOrDefaultAsync(U => U.UserName == loginDTO.Usuario.ToLower());

            if (usuario == null) return Unauthorized("Usuario inválido!");
            var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, loginDTO.Password, false);

            if(!resultado.Succeeded) return Unauthorized("Usuario no encontrado o la contraseña es incorrecta!");

            return Ok
            (
                new UsuarioNuevoDTO
                {
                    Usuario = usuario.UserName,
                    Email = usuario.Email,
                    Token = _tokenService.CrearToken(usuario)
                }
            );
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarDTO registrar)
        {
            try {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var Usuario = new User{
                    UserName = registrar.Usuario,
                    Email = registrar.Email                    
                };

                var crearUsuario = await _userManager.CreateAsync(Usuario, registrar.Password);
                if(crearUsuario.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(Usuario, "User");
                    if(roleResult.Succeeded)
                    {
                        return Ok
                        (
                            new UsuarioNuevoDTO
                            {
                                Usuario = Usuario.UserName,
                                Email = Usuario.Email,
                                Token = _tokenService.CrearToken(Usuario)   
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, crearUsuario.Errors);
                }

            } 
            catch(Exception e) 
            {
                return StatusCode(500, e);
            }
        }
    }
}