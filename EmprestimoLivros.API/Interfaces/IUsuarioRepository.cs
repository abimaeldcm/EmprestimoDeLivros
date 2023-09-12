using EmprestimoLivros.API.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmprestimoLivros.API.Interfaces
{
    public interface IUsuarioRepository
    {
        void incluir(Usuario usuario);
        void Alterar(Usuario usuario);
        Task<bool> Excluir(int id);
        Task<Usuario> BuscarPorId(int id);
        Task<IEnumerable<Usuario>> SelecionarTodos();
    }
}
