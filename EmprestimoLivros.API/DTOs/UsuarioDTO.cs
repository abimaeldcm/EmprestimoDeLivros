using System;
using System.ComponentModel.DataAnnotations;

namespace EmprestimoLivros.API.DTOs
{
    //Data Transfer Object (DTO), em português "Objeto de Transferência de Dados"
    public class UsuarioDTO
    {
        public int UsuarioId { get; set; }
        [Required]
        [MinLength(3, ErrorMessage= "O nome deve ter no mínimo 3 letras")]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Telefone { get; set; }
        public DateTime? DataCadastro { get; set; } = DateTime.Now;
    }
}
