using System.ComponentModel.DataAnnotations;

namespace CrudCarros.Models
{
    public class Concessionaria
    {
        [Key]
        public Guid ConcessionariaId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Nome { get; set; }

        [Required]
        public string? Rua { get; set; }

        [Required]
        public string? Cidade { get; set; }

        [Required]
        public string? Estado { get; set; }

        [Required]
        [RegularExpression("\\d{5}-\\d{3}", ErrorMessage = "O CEP deve estar no formato 00000-000.")]
        public string? CEP { get; set; }

        [Required]
        [Phone]
        public string? Telefone { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A capacidade máxima deve ser um valor inteiro positivo.")]
        public int CapacidadeMaximaVeiculos { get; set; }
    }
}