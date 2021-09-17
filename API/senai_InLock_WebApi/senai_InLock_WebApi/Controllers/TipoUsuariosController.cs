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
    [Produces("application/json")]

    [Route("api/[controller]")]

    [ApiController]

    public class TipoUsuariosController : ControllerBase
    {
        private ITipoUsuarioRepository _TipoUsuarioRepository { get; set; }

        public TipoUsuariosController()
        {
            _TipoUsuarioRepository = new TipoUsuarioRepository();
        }

        [HttpGet]

        public IActionResult Get()
        {
            List<TipoUsuarioDomain> ListaTipoUsuario = _TipoUsuarioRepository.ListarTodos();

            return Ok(ListaTipoUsuario);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            TipoUsuarioDomain tipoUsuarioBuscado = _TipoUsuarioRepository.BuscarPorId(id);

            if (tipoUsuarioBuscado == null)
            {
                return NotFound("Nenhum tipo de usuário encontrado!");
            }

            return Ok(tipoUsuarioBuscado);
        }

        [HttpPost]
        public IActionResult Post(TipoUsuarioDomain novoTipoUsuario)
        {
            _TipoUsuarioRepository.Cadastrar(novoTipoUsuario);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult PutIdUrl(int id, TipoUsuarioDomain tipoUsuarioAtualizado)
        {
            TipoUsuarioDomain tipoUsuarioBuscado = _TipoUsuarioRepository.BuscarPorId(id);

            if (tipoUsuarioBuscado == null)
            {
                return NotFound
                    (new
                    {
                        mensagem = "Tipo de Usuário não encontrado!",
                        erro = true
                    });
            }

            try
            {
                _TipoUsuarioRepository.AtualizarIdUrl(id, tipoUsuarioAtualizado);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [HttpPut]
        public IActionResult PutBody(TipoUsuarioDomain tipoUsuarioAtualizado)
        {
            if (tipoUsuarioAtualizado.tituloUsuario == null || tipoUsuarioAtualizado.idTipoUsuario == 0)
            {
                return BadRequest(
                    new
                    {
                        mensagemErro = "Nome do Tipo de Usuário ou id do Tipo de Usuário não foi informado!"
                    }
                );
            }

            TipoUsuarioDomain tipoUsuarioBuscado = _TipoUsuarioRepository.BuscarPorId(tipoUsuarioAtualizado.idTipoUsuario);

            if (tipoUsuarioBuscado != null)
            {
                try
                {
                    _TipoUsuarioRepository.AtualizarIdCorpo(tipoUsuarioAtualizado);

                    return NoContent();
                }
                catch (Exception erro)
                {
                    return BadRequest(erro);
                }
            }

            return NotFound(
                    new
                    {
                        mensagemErro = "Tipo de Usuário não encontrado!",
                        codErro = true
                    }
                );
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _TipoUsuarioRepository.Deletar(id);

            return StatusCode(204);
        }
    }
}

