using Microsoft.AspNetCore.Mvc;
using senai_InLock_WebApi.Domains;
using senai_InLock_WebApi.Interfaces;
using senai_InLock_WebApi.Repositories;
using System;
using System.Collections.Generic;

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

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            UsuarioDomain usuarioBuscado = _UsuarioRepository.BuscarPorId(id);

            if (usuarioBuscado == null)
            {
                return NotFound("Nenhum genero encontrado!");
            }

            return Ok(usuarioBuscado);
        }

        [HttpPost]
        public IActionResult Post(UsuarioDomain novoUsuario)
        {
            _UsuarioRepository.Cadastrar(novoUsuario);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult PutIdUrl(int id, UsuarioDomain usuarioAtualizado)
        {
            UsuarioDomain UsuarioBuscado = _UsuarioRepository.BuscarPorId(id);

            if (UsuarioBuscado == null)
            {
                return NotFound
                    (new
                    {
                        mensagem = "Usuario não encontrado!",
                        erro = true
                    });
            }

            try
            {
                _UsuarioRepository.AtualizarIdUrl(id, usuarioAtualizado);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [HttpPut]
        public IActionResult PutBody(UsuarioDomain usuarioAtualizado)
        {
            if (usuarioAtualizado.nome == null || usuarioAtualizado.idUsuario == 0)
            {
                return BadRequest(
                    new
                    {
                        mensagemErro = "Nome do Usuario ou id do Usuario não foi informado!"
                    }
                );
            }

            UsuarioDomain usuarioBuscado = _UsuarioRepository.BuscarPorId(usuarioAtualizado.idUsuario);

            if (usuarioBuscado != null)
            {
                try
                {
                    _UsuarioRepository.AtualizarIdCorpo(usuarioAtualizado);

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
                        mensagemErro = "Usuario não encontrado!",
                        codErro = true
                    }
                );
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _UsuarioRepository.Deletar(id);

            return StatusCode(204);
        }
    }
}
