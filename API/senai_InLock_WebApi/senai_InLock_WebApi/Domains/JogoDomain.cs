using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_InLock_WebApi.Domains
{
    public class JogoDomain
    {
        public int idJogo { get; set; }
        public EstudioDomain Estudio { get; set; }
        public string nomeJogo { get; set; }
        public string descricao { get; set; }
        public DateTime dataLancamento { get; set; }
        public double valor { get; set; }
    }
}
