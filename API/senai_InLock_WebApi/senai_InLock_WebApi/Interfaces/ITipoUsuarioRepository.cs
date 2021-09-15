using senai_InLock_WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_InLock_WebApi.Interfaces
{
    interface ITipoUsuarioRepository
    {
        //Lista todos
        List<TipoUsuarioDomain> ListarTodos();

        //Buscar Pelo Id
        TipoUsuarioDomain BuscarPorId(int idTipoUsuario);

        //Cadastrar um Jogo
        void Cadastrar(TipoUsuarioDomain novoTipoUsuario);

        //Atualizar pelo corpo da requisição(JSON)
        void AtualizarIdCorpo(TipoUsuarioDomain tipoUsuarioAtualizado);

        //Atualizar pela Url
        void AtualizarIdUrl(int idEstudio, TipoUsuarioDomain tipoUsuarioAtualizado);

        //Deletar um Jogo
        void Deletar(int idTipoUsuario);
    }
}
