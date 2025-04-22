using System.ComponentModel.DataAnnotations;

namespace CrudCarros.Models.DbContext
{
    public class Veiculo
    {
        [Key]
        public Guid VeiculoId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Nome { get; set; }

        [Required]
        [Range(1900, int.MaxValue, ErrorMessage = "Ano de fabricação inválido.")]
        public int AnoFabricacao { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser um valor positivo.")]
        public decimal Preco { get; set; }

        [Required]
        public int FabricanteId { get; set; }
        public Fabricante? Fabricante { get; set; }

        [Required]
        public TipoVeiculo Tipo { get; set; }

        [MaxLength(500)]
        public string? Descricao { get; set; }
    }

    public enum TipoVeiculo
    {
        Carro,
        Moto,
        Caminhao
    }
}