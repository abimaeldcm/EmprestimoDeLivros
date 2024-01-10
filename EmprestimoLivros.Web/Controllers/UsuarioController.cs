using EmprestimoLivros.Web.Models;
using EmprestimoLivros.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimoLivros.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuariosService _usuarioService;

        public UsuarioController(IUsuariosService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var usuarios = await _usuarioService.SelecionarTodosUsuarios();
            return View(usuarios);
        }
        [HttpGet]
        public ActionResult AdicionarUsuario()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AdicionarUsuario(Usuario usarioAdicionar)
        {
            if (await _usuarioService.Incluir(usarioAdicionar))
            {
                return RedirectToAction(nameof(Index));
            }
            return View(usarioAdicionar);
        }
        [HttpGet]
        public async Task<ActionResult> EditarUsuario(int id)
        {
            var usuario = await _usuarioService.BuscarPorId(id);
            if (usuario is not null)
            {
                return View(usuario);
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> EditarUsuario(Usuario usarioEditar)
        {
            if (await _usuarioService.Alterar(usarioEditar))
            {
                return RedirectToAction(nameof(Index));
            }
            return View(usarioEditar);
        }
        [HttpPost]
        public async Task<ActionResult> DeletarUsuario(int id)
        {
            if (await _usuarioService.Excluir(id))
            {
                return RedirectToAction(nameof(Index));
            }
            return View("Index");
        }
    }
}
