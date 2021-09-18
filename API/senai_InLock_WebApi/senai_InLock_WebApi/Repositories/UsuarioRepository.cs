using senai_InLock_WebApi.Domains;
using senai_InLock_WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace senai_InLock_WebApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        //private string stringConexao = @"Data Source=NOTE0113C5\SQLEXPRESS; initial catalog=inlock_games_manha; user Id=sa; pwd=Senai@132";
        private string stringConexao = @"Data Source=MARCAUM\SQLEXPRESS; initial catalog=inlock_games_manha; user Id=sa; pwd=senai@132";
        public void AtualizarIdCorpo(UsuarioDomain usuarioAtualizado)
        {
            if (usuarioAtualizado.nome != null)
            {
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    string queryUpdateBody = "UPDATE usuario SET nome = @novoNomeUsuario, email = @novoEmailUsuario, senha = @novaSenhaUsuario WHERE idUsuario = @idUsuario";

                    using (SqlCommand cmd = new SqlCommand(queryUpdateBody, con))
                    {
                        cmd.Parameters.AddWithValue("@novoNomeUsuario", usuarioAtualizado.nome);
                        cmd.Parameters.AddWithValue("@novoEmailUsuario", usuarioAtualizado.email);
                        cmd.Parameters.AddWithValue("@novaSenhaUsuario", usuarioAtualizado.senha);
                        cmd.Parameters.AddWithValue("@idUsuario", usuarioAtualizado.idUsuario);

                        con.Open();

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void AtualizarIdUrl(int idUsuario, UsuarioDomain usuarioAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateUrl = "UPDATE usuario SET nome = @novoNomeUsuario, email = @novoEmailUsuario, senha = @novaSenhaUsuario WHERE idUsuario = @idUsuario";

                using (SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                {
                    cmd.Parameters.AddWithValue("@novoNomeUsuario", usuarioAtualizado.nome);
                    cmd.Parameters.AddWithValue("@novoEmailUsuario", usuarioAtualizado.email);
                    cmd.Parameters.AddWithValue("@novaSenhaUsuario", usuarioAtualizado.senha);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public UsuarioDomain BuscarPorEmailSenha(string email, string senha)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelect = @"SELECT idUsuario, nome, email, senha, tu.idTipoUsuario, tituloUsuario FROM usuario INNER JOIN tipoUsuario tu ON usuario.idTipoUsuario = tu.idTipoUsuario WHERE email  = @email AND senha = @senha";

                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        UsuarioDomain usuarioBuscado = new UsuarioDomain
                        {
                            idUsuario = Convert.ToInt32(rdr["idUsuario"]),
                            nome = rdr["nome"].ToString(),
                            email = rdr["email"].ToString(),
                            senha = rdr["senha"].ToString(),
                            TipoUsuario = new TipoUsuarioDomain
                            {
                                idTipoUsuario = Convert.ToInt32(rdr["idTipoUsuario"]),
                                tituloUsuario = rdr["tituloUsuario"].ToString()
                            }
                        };
                        return usuarioBuscado;
                    }
                    return null;
                }
            } 
        }
        public UsuarioDomain BuscarPorId(int idUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT idUsuario, nome, email, senha, tu.idTipoUsuario, tituloUsuario FROM usuario INNER JOIN tipoUsuario tu ON usuario.idTipoUsuario = tu.idTipoUsuario WHERE idUsuario = @idUsuario";

                con.Open();

                SqlDataReader reader;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        UsuarioDomain usuarioBuscado = new UsuarioDomain
                        {
                            idUsuario = Convert.ToInt32(reader["idUsuario"]),
                            nome = reader["nome"].ToString(),
                            email = reader["email"].ToString(),
                            senha = reader["senha"].ToString(),
                            TipoUsuario = new TipoUsuarioDomain()
                            {
                                idTipoUsuario = Convert.ToInt32(reader["idTipoUsuario"]),
                                tituloUsuario = reader["tituloUsuario"].ToString()
                            }
                        };
                        return usuarioBuscado;
                    }
                    return null;
                }
            };
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
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM usuario WHERE idUsuario = @id";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@id", idUsuario);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
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
                                tituloUsuario = rdr["tituloUsuario"].ToString()
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
