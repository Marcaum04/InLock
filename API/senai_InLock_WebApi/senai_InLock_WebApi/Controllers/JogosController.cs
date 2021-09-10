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
    [Produces("application/json")]

    [Route("api/[controller]")]

    [ApiController]
    public class JogosController : ControllerBase
    {
        private IJogoRepository _JogoRepository { get; set; }
        
        public JogosController()
        {
            _JogoRepository = new JogoRepository();
        }

        [HttpGet]

        public IActionResult Get()
        {
            List<JogoDomain> ListaJogos = _JogoRepository.ListarTodos();

            return Ok(ListaJogos);
        }
    }
}
