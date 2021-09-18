using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]

        public IActionResult Get()
        {
            List<JogoDomain> ListaJogos = _JogoRepository.ListarTodos();

            return Ok(ListaJogos);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            JogoDomain jogoBuscado = _JogoRepository.BuscarPorId(id);

            if (jogoBuscado == null)
            {
                return NotFound("Nenhum genero encontrado!");
            }

            return Ok(jogoBuscado);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public IActionResult Post(JogoDomain novoJogo)
        {
            _JogoRepository.Cadastrar(novoJogo);

            return StatusCode(201);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public IActionResult PutIdUrl(int id, JogoDomain jogoAtualizado)
        {
            JogoDomain jogoBuscado = _JogoRepository.BuscarPorId(id);

            if (jogoBuscado == null)
            {
                return NotFound
                    (new
                    {
                        mensagem = "Jogo não encontrado!",
                        erro = true
                    });
            }

            try
            {
                _JogoRepository.AtualizarIdUrl(id, jogoAtualizado);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut]
        public IActionResult PutBody(JogoDomain jogoAtualizado)
        {
            if (jogoAtualizado.nomeJogo == null || jogoAtualizado.idJogo == 0)
            {
                return BadRequest(
                    new
                    {
                        mensagemErro = "Nome do Jogo ou id do Jogo não foi informado!"
                    }
                );
            }

            JogoDomain jogoBuscado = _JogoRepository.BuscarPorId(jogoAtualizado.idJogo);

            if (jogoBuscado != null)
            {
                try
                {
                    _JogoRepository.AtualizarIdCorpo(jogoAtualizado);

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
                        mensagemErro = "Jogo não encontrado!",
                        codErro = true
                    }
                );
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _JogoRepository.Deletar(id);

            return StatusCode(204);
        }
    }
}
