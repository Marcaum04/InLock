using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai_InLock_WebApi.Domains;
using senai_InLock_WebApi.Interfaces;
using senai_InLock_WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_InLock_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IUsuarioRepository _UsuarioRepository { get; set; }

        public UsuariosController()
        {
            _UsuarioRepository = new UsuarioRepository();
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            List<UsuarioDomain> ListaUsuarios = _UsuarioRepository.ListarTodos();

            return Ok(ListaUsuarios);
        }

        [HttpPost]
        public IActionResult Post(UsuarioDomain novoUsuario)
        {
            _UsuarioRepository.Cadastrar(novoUsuario);

            return StatusCode(201);
        }
    }
}
