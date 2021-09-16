using senai_InLock_WebApi.Domains;
using senai_InLock_WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai_InLock_WebApi.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {

        private string stringConexao = @"Data Source=NOTE0113A1\SQLEXPRESS; initial catalog=inlock_games_manha; user Id=sa; pwd=Senai@132";

        public void AtualizarIdCorpo(TipoUsuarioDomain tipoUsuarioAtualizado)
        {
            if (tipoUsuarioAtualizado.tituloUsuario != null)
            {
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    string queryUpdateBody = "UPDATE tipoUsuario SET tituloUsuario = @novoTipoUsuario WHERE idTipoUsuario = @tipoUsuarioAtualizado";

                    using (SqlCommand cmd = new SqlCommand(queryUpdateBody, con))
                    {
                        cmd.Parameters.AddWithValue("@novoTipoUsuario", tipoUsuarioAtualizado.tituloUsuario);
                        cmd.Parameters.AddWithValue("@tipoUsuarioAtualizado", tipoUsuarioAtualizado.idTipoUsuario);

                        con.Open();

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void AtualizarIdUrl(int idTipoUsuario, TipoUsuarioDomain tipoUsuarioAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateUrl = "UPDATE tipoUsuario SET tituloUsuario = @novoTipoUsuario WHERE idTipoUsuario = @tipoUsuarioAtualizado";

                using (SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                {
                    cmd.Parameters.AddWithValue("@novoTipoUsuario", tipoUsuarioAtualizado.tituloUsuario);
                    cmd.Parameters.AddWithValue("@tipoUsuarioAtualizado", idTipoUsuario);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public TipoUsuarioDomain BuscarPorId(int idTipoUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT idTipoUsuario, tituloUsuario FROM tipoUsuario WHERE idTipoUSuario = @idTipoUsuario";

                con.Open();

                SqlDataReader reader;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idTipoUsuario", idTipoUsuario);

                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        TipoUsuarioDomain tipoUsuarioBuscado = new TipoUsuarioDomain
                        {
                            idTipoUsuario = Convert.ToInt32(reader["idTipoUsuario"]),
                            tituloUsuario = reader["tituloUsuario"].ToString()
                        };
                        return tipoUsuarioBuscado;
                    }
                    return null;
                }
            }
        }

        public void Cadastrar(TipoUsuarioDomain novoTipoUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO tipoUsuario (tituloUsuario) VALUES (@tituloUsuario)";


                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {

                    cmd.Parameters.AddWithValue("@tituloUsuario", novoTipoUsuario.tituloUsuario);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idTipoUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM tipoUsuario WHERE idTipoUsuario = @id";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@id", idTipoUsuario);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<TipoUsuarioDomain> ListarTodos()
        {
            List<TipoUsuarioDomain> ListaTipoUsuario = new List<TipoUsuarioDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string QuerySelectAll = "SELECT idTipoUsuario, tituloUsuario FROM tipoUsuario";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(QuerySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        TipoUsuarioDomain TipoUsuario = new TipoUsuarioDomain()
                        {
                            idTipoUsuario = Convert.ToInt32(rdr[0]),
                            tituloUsuario = rdr[1].ToString(),
                        };
                        ListaTipoUsuario.Add(TipoUsuario);
                    }
                }
            };
            return ListaTipoUsuario;
        }
    }
}
