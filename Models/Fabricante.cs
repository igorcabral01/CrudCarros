using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CrudCarros.Models.DbContext
{
    public class Fabricante
    {
        [Key]
        public Guid FabricanteId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Nome { get; set; }

        [Required]
        [MaxLength(50)]
        public string? PaisDeOrigem { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Ano de Fundação deve ser um ano válido no passado.")]
        public int AnoDeFundacao { get; set; }

        [Url]
        public string? Website { get; set; }
    }
}