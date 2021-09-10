using senai_InLock_WebApi.Domains;
using senai_InLock_WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai_InLock_WebApi.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private string stringConexao = @"Data Source=NOTE0113C5\SQLEXPRESS; initial catalog=inlock_games_manha; user Id=sa; pwd=Senai@132";
        public void AtualizarIdCorpo(JogoDomain jogoAtualizado)
        {
            throw new NotImplementedException();
        }

        public void AtualizarIdUrl(int idJogo, JogoDomain jogoAtualizado)
        {
            throw new NotImplementedException();
        }

        public JogoDomain BuscarPorId(int idJogo)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(JogoDomain novoJogo)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int idJogo)
        {
            throw new NotImplementedException();
        }

        public List<JogoDomain> ListarTodos()
        {
            List<JogoDomain> ListaJogos = new List<JogoDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string QuerySelectAll = "SELECT idJogos, nomeJogo, descricao, dataLancamento, valor, e.idEstudio, nomeEstudio FROM jogos INNER JOIN estudio e ON jogos.idEstudio = e.idEstudio";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(QuerySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while(rdr.Read())
                    {
                        JogoDomain jogo = new JogoDomain()
                        {
                            idJogo = Convert.ToInt32(rdr[0]),
                            nomeJogo = rdr[1].ToString(),
                            descricao = rdr[2].ToString(),
                            dataLancamento = Convert.ToDateTime(rdr[3]),
                            valor = Convert.ToDouble(rdr[4]),
                            Estudio = new EstudioDomain
                            {
                                idEstudio = Convert.ToInt32(rdr[5]),
                                nomeEstudio = rdr[6].ToString()
                            },
                        };
                        ListaJogos.Add(jogo);
                    }
                }
            };
            return ListaJogos;
        }
    }
}
