using EmprestimoLivros.Web.Models;

namespace EmprestimoLivros.Web.Services
{
    public interface IUsuariosService
    {   
        Task<IEnumerable<Usuario>> SelecionarTodosUsuarios();
        Task<bool> Incluir(Usuario usuario);
        Task<bool> Alterar(Usuario usuario);
        Task<bool> Excluir(int id);
        Task<Usuario> BuscarPorId(int id);
        
    }
}
