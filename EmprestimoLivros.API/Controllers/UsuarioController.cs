using AutoMapper;
using EmprestimoLivros.API.DTOs;
using EmprestimoLivros.API.Interfaces;
using EmprestimoLivros.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmprestimoLivros.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        [HttpGet("SelecionarTodos")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario()
        {
            var usuarios = await _usuarioRepository.SelecionarTodos();
            if (usuarios.Any())
            {
               return Ok(usuarios);
            }
            return BadRequest("Erro ao localizar os usuários.");
        }

        [HttpGet("BuscarUsuarioId/{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(int id)
        {
            Usuario usuario = await _usuarioRepository.BuscarPorId(id);
            if (usuario == null)
            {

                return NotFound("Erro ao localizar o usuário informado");
            }

            //Par que tipo você quer mapear (UsuarioDTO) de que tipo (usuario)
            var UsuarioDTO = _mapper.Map<UsuarioDTO>(usuario);

            return Ok(UsuarioDTO);


        }

        [HttpPost("Cadastrar")]
        public ActionResult CadastrarUsuario(Usuario usuario)
        {
            _usuarioRepository.incluir(usuario);
            return Ok("Deu Certo!!!");
        }

        [HttpPut("Alterar")]
        public ActionResult AlterarUsuario(Usuario usuario)
        {
            _usuarioRepository.Alterar(usuario);
            return Ok($"Usuário {usuario.Nome} alterado com Sucesso!");
        }
        [HttpDelete("Deletar/{id}")]
        public async Task<ActionResult> RemoverUsuario(int id)
        {
            bool resExclusao = await _usuarioRepository.Excluir(id);
            if (resExclusao)
            {
                return Ok("Excluido com sucesso!!!");
            }

            return BadRequest("Erro ao realizar a exclusão do usuário");
            
            
        }
    }
}
