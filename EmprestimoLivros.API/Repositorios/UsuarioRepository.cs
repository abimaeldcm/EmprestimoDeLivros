using EmprestimoLivros.API.Data;
using EmprestimoLivros.API.Interfaces;
using EmprestimoLivros.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public void Alterar(Usuario  usuario)
        {
            try
            {
                _emprestimosContext.Update(usuario);
                _emprestimosContext.SaveChanges();
            }
            catch (Exception erro)
            {

                throw new Exception("Erro ao alterar o usuário. \nDetalhes: " + erro.Message );
            }
            
        }

        public async Task<bool> Excluir(int id)
        {
            try
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
            catch (Exception erro)
            {
                throw new Exception (erro.Message);
            }
        }

        public void incluir(Usuario usuario)
        {
            try
            {
                _emprestimosContext.Usuarios.Add(usuario);
                _emprestimosContext.SaveChanges();

            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }

        }

        public async Task<Usuario> BuscarPorId(int id)
        {
            try
            {
                return await _emprestimosContext.Usuarios.FindAsync(id);
            }
            catch (Exception erro) 
            { 
                throw new Exception(erro.Message); 
            }
        }

        public async Task<IEnumerable<Usuario>> SelecionarTodos()
        {
            try
            {
                return await _emprestimosContext.Usuarios.ToListAsync();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
    }
}
