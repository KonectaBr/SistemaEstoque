using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estoque.Domain.Entities;

namespace Estoque.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        public IEnumerable<Usuario> ProcurarUsuario(string nome);
        public Usuario Login(string nome, string senha);
    }
}
