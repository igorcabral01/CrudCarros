using System.ComponentModel;
using System.ComponentModel.DataAnnotations; 
using CrudCarros.Models.DbContext;

namespace CrudCarros.Models
{
    public class Usuario : EntidadeBase
    {
        public Guid UsuarioId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email deve ser válido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        public string? Senha { get; set; }

        [Required(ErrorMessage = "O tipo de usuário é obrigatório.")]
        public TipoUsuario TipoUsuario { get; set; }
    }

    public enum TipoUsuario
    {
        [Description("Administrador")]
        Administrador = 0,
        [Description("Gerente")]
        Gerente = 1,
        [Description("Vendedor")]
        Vendedor = 2,
    }
}