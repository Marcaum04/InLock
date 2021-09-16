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
            if (tipoUsuarioAtualizado.tipoUsuario != null)
            {
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    string queryUpdateBody = "UPDATE tipoUsuario SET tipoUsuario = @novoTipoUsuario WHERE idTipoUsuario = @tipoUsuarioAtualizado";

                    using (SqlCommand cmd = new SqlCommand(queryUpdateBody, con))
                    {
                        cmd.Parameters.AddWithValue("@novoTipoUsuario", tipoUsuarioAtualizado.tipoUsuario);
                        cmd.Parameters.AddWithValue("@idTipoUsuario", tipoUsuarioAtualizado.idTipoUsuario);

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
                string queryUpdateUrl = "UPDATE tipoUsuario SET tipoUsuario = @novoTipoUsuario WHERE idTipoUsuario = @tipoUsuarioAtualizado";

                using (SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                {
                    cmd.Parameters.AddWithValue("@novoTipoUsuario", tipoUsuarioAtualizado.tipoUsuario);
                    cmd.Parameters.AddWithValue("@idEstudioAtualizado", idTipoUsuario);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public TipoUsuarioDomain BuscarPorId(int idTipoUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT idTipoUsuario, tipoUsuario FROM tipoUsuario WHERE idTipoUSuario = @idTipoUsuario";

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
                            tipoUsuario = reader["tipoUsuario"].ToString()
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
                string queryInsert = "INSERT INTO tipoUsuario (tipoUsuario) VALUES (@tipoUsuario)";


                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {

                    cmd.Parameters.AddWithValue("@tipoUsuario", novoTipoUsuario.tipoUsuario);

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
                string QuerySelectAll = "SELECT idTipoUsuario, tipoUsuario FROM tipoUsuario";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(QuerySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        TipoUsuarioDomain tipoUsuario = new TipoUsuarioDomain()
                        {
                            idTipoUsuario = Convert.ToInt32(rdr[0]),
                            tipoUsuario = rdr[1].ToString(),
                        };
                        ListaTipoUsuario.Add(tipoUsuario);
                    }
                }
            };
            return ListaTipoUsuario;
        }
    }
}
