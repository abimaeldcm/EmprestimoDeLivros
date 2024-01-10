using Microsoft.Build.Framework;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.ComponentModel;

#nullable disable

namespace EmprestimoLivros.Web.Models
{
    public class Livro
    {
        [DisplayName("Id")]
        public int LivroId { get; set; }
        [DisplayName("Título")]
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Autor { get; set; }
        [Required]
        public string ImagemCapa { get; set; }
        [DisplayName("Ano de publicação")]
        [Required]
        public int? AnoPublicacao { get; set; }
        public bool Disponivel { get; set; }
    }
}
