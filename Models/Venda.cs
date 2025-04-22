using System.ComponentModel.DataAnnotations;
using CrudCarros.Models.DbContext;

namespace CrudCarros.Models
{
    public class Venda
    {
        [Key]
        public Guid VendaId { get; set; }

        [Required]
        public int ConcessionariaId { get; set; }
        public Concessionaria? Concessionaria { get; set; }

        [Required]
        public int FabricanteId { get; set; }
        public Fabricante? Fabricante { get; set; }

        [Required]
        public int VeiculoId { get; set; }
        public Veiculo? veiculo { get; set; }

        [Required]
        public string? ClienteNome { get; set; }

        [Required]
        [RegularExpression("\\d{3}\\.\\d{3}\\.\\d{3}-\\d{2}", ErrorMessage = "O CPF deve estar no formato 000.000.000-00.")]
        public string? ClienteCPF { get; set; }

        [Required]
        [Phone]
        public string? ClienteTelefone { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Venda), nameof(ValidarDataVenda))]
        public DateTime DataVenda { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "O preço de venda deve ser um valor positivo.")]
        public decimal PrecoVenda { get; set; }

        [Required]
        public string? NumeroProtocolo { get; set; }

        public Venda()
        {
            NumeroProtocolo = GerarNumeroProtocoloUnico();
        }

        private static string GerarNumeroProtocoloUnico()
        {
            return new Random().Next(1000000000, 2000000000).ToString();
        }

        public static ValidationResult ValidarDataVenda(DateTime dataVenda, ValidationContext context)
        {
            if (dataVenda > DateTime.Now)
            {
                return new ValidationResult("A data da venda não pode ser futura.");
            }
            return ValidationResult.Success!;
        }
    }
}