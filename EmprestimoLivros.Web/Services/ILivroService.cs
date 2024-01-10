using EmprestimoLivros.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimoLivros.Web.Services
{
    public interface ILivrosService
    {   
        Task<IEnumerable<Livro>> SelecionarTodosOsLivros();
        Task<bool> Incluir(Livro usuario);
        Task<bool> Alterar(Livro usuario);
        Task<bool> Excluir(int id);
        Task<Livro> BuscarLivroPorId(int id);
        Task<IEnumerable<Livro>> BuscarLivroPorNome(string nome);
        string ImagemUpload(IFormFile foto);
    }
}
