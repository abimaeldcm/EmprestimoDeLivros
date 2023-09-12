using System;
using System.Collections.Generic;

#nullable disable

namespace EmprestimoLivros.API.Models
{
    public partial class Livro
    {
        public Livro()
        {
            Emprestimos = new HashSet<Emprestimo>();
        }

        public int LivroId { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int? AnoPublicacao { get; set; }
        public bool? Disponivel { get; set; }

        public virtual ICollection<Emprestimo> Emprestimos { get; set; }
    }
}
