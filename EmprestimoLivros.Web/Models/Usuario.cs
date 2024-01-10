using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmprestimoLivros.Web.Models
{
    public class Usuario
    {
        [DisplayName("Id")]
        public int UsuarioId { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 letras")]
        public string Nome { get; set; }

        [Required]
        [DisplayName("E-mail")]
        [EmailAddress(ErrorMessage = "Digite um e-mail válido")]
        public string Email { get; set; }

        [Required]
        public string Telefone { get; set; }
        public DateTime? DataCadastro { get; set; } = DateTime.Now;
    }
}
