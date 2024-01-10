using System;
using System.Collections.Generic;

#nullable disable

namespace EmprestimoLivros.API.Models
{
    public class Emprestimo
    {
        public int EmprestimoId { get; set; }
        public int? LivroId { get; set; }
        public int? UsuarioId { get; set; }
        public DateTime? DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public virtual Livro Livro { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
