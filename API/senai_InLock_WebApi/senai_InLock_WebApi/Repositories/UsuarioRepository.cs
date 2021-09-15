using senai_InLock_WebApi.Domains;
using senai_InLock_WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai_InLock_WebApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string stringConexao = @"Data Source=NOTE0113C5\SQLEXPRESS; initial catalog=inlock_games_manha; user Id=sa; pwd=Senai@132";
        public void AtualizarIdCorpo(UsuarioDomain usuarioAtualizado)
        {
            throw new NotImplementedException();
        }

        public void AtualizarIdUrl(int idUsuario, UsuarioDomain usuarioAtualizado)
        {
            throw new NotImplementedException();
        }

        public UsuarioDomain BuscarPorId(int idUsuario)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(UsuarioDomain novoUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO usuario (nome, email, senha, idTipoUsuario) VALUES (@nomeUsuario, @emailUsuario, @senhaUsuario, @idTipoUsuario)";


                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {

                    cmd.Parameters.AddWithValue("@nomeUsuario", novoUsuario.nome);
                    cmd.Parameters.AddWithValue("@emailUsuario", novoUsuario.email);
                    cmd.Parameters.AddWithValue("@senhaUsuario", novoUsuario.senha);
                    cmd.Parameters.AddWithValue("@idTipoUsuario", novoUsuario.TipoUsuario.idTipoUsuario);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idUsuario)
        {
            throw new NotImplementedException();
        }

        public List<UsuarioDomain> ListarTodos()
        {
            List<UsuarioDomain> ListaUsuarios = new List<UsuarioDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string QuerySelectAll = "SELECT idUsuario, nome, email, senha, tu.idTipoUsuario, tituloUsuario FROM usuario INNER JOIN tipoUsuario tu ON usuario.idTipoUsuario = tu.idTipoUsuario";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(QuerySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        UsuarioDomain usuario = new UsuarioDomain()
                        {
                            idUsuario = Convert.ToInt32(rdr[0]),
                            nome = rdr["nome"].ToString(),
                            email = rdr["email"].ToString(),
                            senha = rdr["senha"].ToString(),
                            TipoUsuario = new TipoUsuarioDomain
                            {
                                idTipoUsuario = Convert.ToInt32(rdr["idTipoUsuario"]),
                                tipoUsuario = rdr["tituloUsuario"].ToString()
                            },
                        };
                        ListaUsuarios.Add(usuario);
                    }
                }
            };
            return ListaUsuarios;
        }
    }
}
