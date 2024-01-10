using System.ComponentModel.DataAnnotations;

namespace EmprestimoLivros.API.DTOs
{
    //Data Transfer Object (DTO), em português "Objeto de Transferência de Dados"
    public class LivroDTO
    {
        public int LivroId { get; set; }
        [Required]
        public string Titulo { get; set; }
        public string Autor { get; set; }
        [Required]
        public int? AnoPublicacao { get; set; }
        public string ImagemCapa { get; set; }
        public bool? Disponivel { get; set; }
    }
}
