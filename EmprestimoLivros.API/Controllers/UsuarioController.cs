using AutoMapper;
using EmprestimoLivros.API.DTOs;
using EmprestimoLivros.API.Interfaces;
using EmprestimoLivros.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
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
            try 
            { 
                var usuarios = await _usuarioRepository.SelecionarTodos();

                var usuariosDTO = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios); //O mapper já faz a transformação da lista por completo sem precisar de laços

                if (usuarios.Any())
                {
                    return Ok(usuariosDTO);
                }
                return BadRequest("Erro ao localizar os usuários.");
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [HttpGet("BuscarUsuarioId/{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(int id)
        {
            try
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
            catch (Exception erro)
            {
                return BadRequest(erro);
            }

        }

        [HttpPost("Cadastrar")]
        public ActionResult CadastrarUsuario(UsuarioDTO usuarioDTO)
        {
            try 
            { 
                //Mapeamento inverso
                var usuario = _mapper.Map<Usuario>(usuarioDTO);

                _usuarioRepository.incluir(usuario);
                return Ok("Deu Certo!!!");
            }
            catch (Exception erro)
            {
                return BadRequest("asasas" + erro.Message);
            }
        }


        [HttpPut("Alterar")]
        public ActionResult AlterarUsuario(UsuarioDTO usuarioDTO)
        {
            try
            {
                if (usuarioDTO.UsuarioId == null)
                {
                    return BadRequest("Não foi possível localizar o usuário. Informe um id");
                }

                var usuario = _mapper.Map<Usuario>(usuarioDTO);

                _usuarioRepository.Alterar(usuario);
                return Ok($"Usuário {usuario.Nome} alterado com Sucesso!");

            }
            catch(System.Exception erro)
            {

                return BadRequest(erro);
            }
            
        }
        [HttpDelete("Deletar/{id}")]
        public async Task<ActionResult> RemoverUsuario(int id)
        {
            try
            {
                bool resExclusao = await _usuarioRepository.Excluir(id);
                if (resExclusao)
                {
                    return Ok("Excluido com sucesso!!!");
                }

                return BadRequest("Erro ao realizar a exclusão do usuário");
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }
    }
}
