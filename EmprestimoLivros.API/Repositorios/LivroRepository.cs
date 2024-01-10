using EmprestimoLivros.API.Data;
using EmprestimoLivros.API.Interfaces;
using EmprestimoLivros.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmprestimoLivros.API.Repositorios
{
    public class LivroRepository : ILivroRepository
    {

        private readonly EmprestimosContext _emprestimosContext;

        public LivroRepository(EmprestimosContext emprestimosContext)
        {
            _emprestimosContext = emprestimosContext;
        }
        public async Task<IEnumerable<Livro>> SelecionarTodos()
        {
            try
            {
                return await _emprestimosContext.Livros.ToListAsync();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        public async Task<IEnumerable<Livro>> BuscarPorNome(string nome)
        {
            try
            {
                return await _emprestimosContext.Livros.Where(l => EF.Functions.Like(l.Titulo, $"%{nome}%"))
                .ToListAsync();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        public async Task<Livro> BuscarPorId(int id)
        {
            try
            {
                return await _emprestimosContext.Livros.FindAsync(id);
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        public async Task Alterar(Livro livro)
        {
            try
            {
                _emprestimosContext.Update(livro);
                await _emprestimosContext.SaveChangesAsync();
            }
            catch (Exception erro)
            {

                throw new Exception("Erro ao alterar o livro. \nDetalhes: " + erro.Message);
            }

        }

        public async Task<bool> Excluir(int id)
        {
            try
            {
                Livro livroDb = await BuscarPorId(id);
                if (livroDb == null)
                {
                    return false;
                }
                _emprestimosContext.Remove(livroDb);
                _emprestimosContext.SaveChanges();

                return true;

            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }

        public async Task Incluir(Livro livro)
        {
            try
            {
                await _emprestimosContext.Livros.AddAsync(livro);
                _emprestimosContext.SaveChanges();

            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        

            
    }        
}
