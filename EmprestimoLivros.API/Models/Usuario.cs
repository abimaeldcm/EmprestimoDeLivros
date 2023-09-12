using System;
using System.Collections.Generic;

#nullable disable

namespace EmprestimoLivros.API.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Emprestimos = new HashSet<Emprestimo>();
        }

        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime? DataCadastro { get; set; } = DateTime.Now;

        public virtual ICollection<Emprestimo>? Emprestimos { get; set; }
    }
}
