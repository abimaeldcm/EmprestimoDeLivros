using System.Collections.Generic;
using System.Threading.Tasks;
using EmprestimoLivros.API.Models;

namespace EmprestimoLivros.API.Interfaces
{
    public interface ILivroRepository
    {
        Task<IEnumerable<Livro>> SelecionarTodos();
        Task<Livro> BuscarPorId(int id);
        Task<IEnumerable<Livro>> BuscarPorNome(string nome);
        Task Incluir(Livro livro);
        Task Alterar(Livro livro);
        Task<bool> Excluir(int id);
    }
}