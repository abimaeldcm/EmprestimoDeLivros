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
    public class LivroController : Controller
    {
        private readonly ILivroRepository _livroRepository;
        private readonly IMapper _mapper;

        public LivroController(ILivroRepository livroRepository, IMapper mapper)
        {
            _livroRepository = livroRepository;
            _mapper = mapper;
        }

        [HttpGet("SelecionarTodos")]
        public async Task<ActionResult<IEnumerable<Livro>>> BuscarTodosLivro()
        {
            try 
            { 
                var livro = await _livroRepository.SelecionarTodos();

                var livroDTO = _mapper.Map<IEnumerable<LivroDTO>>(livro); //O mapper já faz a transformação da lista por completo sem precisar de laços

                if (livro.Any())
                {
                    return Ok(livroDTO);
                }
                return BadRequest("Erro ao localizar os Livros.");
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [HttpGet("BuscarLivroId/{id}")]
        public async Task<ActionResult<LivroDTO>> BuscarLivroId(int id)
        {
            try
            {
                Livro livro = await _livroRepository.BuscarPorId(id);
                if (livro == null)
                {

                    return NotFound("Erro ao localizar o livro informado");
                }

                //Par que tipo você quer mapear (livroDTO) de que tipo (livro)
                var livroDTO = _mapper.Map<LivroDTO>(livro);

                return Ok(livroDTO);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }

        }
        [HttpGet("BuscarLivroNome/{nome}")]
        public async Task<ActionResult<LivroDTO>> BuscarLivroNome(string nome)
        {
            try
            {
                var livro = await _livroRepository.BuscarPorNome(nome);
                if (!livro.Any())
                {
                    return NotFound("Erro ao localizar o livro informado");
                }

                //Par que tipo você quer mapear (livroDTO) de que tipo (livro)
                var livroDTO = _mapper.Map<IEnumerable<LivroDTO>>(livro);

                return Ok(livroDTO);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }

        }

        [HttpPost("Cadastrar")]
        public ActionResult CadastrarLivro(LivroDTO livroDTO)
        {
            try 
            { 
                //Mapeamento inverso
                var livro = _mapper.Map<Livro>(livroDTO);

                _livroRepository.Incluir(livro);
                return Ok();
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
        [HttpPut("Alterar")]
        public ActionResult AlterarLivro(LivroDTO livroDTO)
        {
            try
            {
                if (livroDTO.LivroId == 0)
                {
                    return BadRequest("Não foi possível localizar o livro. Informe um id");
                }

                var livro = _mapper.Map<Livro>(livroDTO);

                _livroRepository.Alterar(livro);
                return Ok($"Livro {livro.Titulo} alterado com Sucesso!");

            }
            catch(System.Exception erro)
            {

                return BadRequest(erro);
            }
            
        }
        [HttpDelete("Deletar/{id}")]
        public async Task<ActionResult> RemoverLivro(int id)
        {
            try
            {
                bool sucessoExclusao = await _livroRepository.Excluir(id);
                if (sucessoExclusao)
                {
                    return Ok("Excluido com sucesso!!!");
                }

                return BadRequest("Erro ao realizar a exclusão do livro");
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }
    }
}
