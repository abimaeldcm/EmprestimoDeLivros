using EmprestimoLivros.API.Data;
using EmprestimoLivros.API.Interfaces;
using EmprestimoLivros.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmprestimoLivros.API.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly EmprestimosContext _emprestimosContext;

        public UsuarioRepository(EmprestimosContext emprestimosContext)
        {
            _emprestimosContext = emprestimosContext;
        }

        public void Alterar(Usuario usuario)
        {
            _emprestimosContext.Update(usuario);
            _emprestimosContext.SaveChanges();
        }

        public async Task<bool> Excluir(int id)
        {
            Usuario usuario = await BuscarPorId(id);
            if (usuario == null)
            {
                return false;
            }

            _emprestimosContext.Remove(usuario);
            _emprestimosContext.SaveChanges();

            return true;
        }

        public void incluir(Usuario usuario)
        {
            _emprestimosContext.Usuarios.Add(usuario);
            _emprestimosContext.SaveChanges();
        }

        public async Task<Usuario> BuscarPorId(int id)
        {
            return await _emprestimosContext.Usuarios.FindAsync(id);
        }

        public async Task<IEnumerable<Usuario>> SelecionarTodos()
        {
            return await _emprestimosContext.Usuarios.ToListAsync();
        }
    }
}
