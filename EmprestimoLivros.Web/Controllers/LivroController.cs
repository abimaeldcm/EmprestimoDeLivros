using EmprestimoLivros.Web.Models;
using EmprestimoLivros.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimoLivros.Web.Controllers
{
    public class LivroController : Controller
    {
        private readonly ILivrosService _livroService;

        public LivroController(ILivrosService livroService)
        {
            _livroService = livroService;
        }
        [HttpGet]
        public async Task<ActionResult> Index(string nome)
        {
            if (!string.IsNullOrEmpty(nome))
            {
                var livro = await _livroService.BuscarLivroPorNome(nome);
                return View(livro);
            }
            var livros = await _livroService.SelecionarTodosOsLivros();
            if (livros == null)
            {
                livros = new List<Livro>(); // Ou inicialize com os dados necessários
            }

            return View(livros);
        }
        [HttpGet]
        public ActionResult AdicionarLivros()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> BuscarLivroId(int id)
        {
            var livro = await _livroService.BuscarLivroPorId(id);
            if (livro is not null)
            {
                return RedirectToAction(nameof(Index), livro);
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AdicionarLivros(Livro usuarioAdicionar, IFormFile fotoCapa)
        {
            string caminhoImagem = _livroService.ImagemUpload(fotoCapa);
            usuarioAdicionar.ImagemCapa = caminhoImagem;

             if (await _livroService.Incluir(usuarioAdicionar))
            {
                return RedirectToAction(nameof(Index));
            }
            return View(usuarioAdicionar);
        }
        [HttpGet]
        public async Task<ActionResult> EditarLivro(int id)
        {
            var livro = await _livroService.BuscarLivroPorId(id);
            if (livro is not null)
            {
                return View(livro);
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> EditarLivro(Livro livroEditar)
        {
            if (await _livroService.Alterar(livroEditar))
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<ActionResult> DeletarLivro(int id)
        {
            if (await _livroService.Excluir(id))
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index");
        }
        
    }
}
