using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_InLock_WebApi.Domains
{
    public class UsuarioDomain
    {
        public int idUsuario { get; set; }
        public TipoUsuarioDomain TipoUsuario { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
    }
}
