using CrudCarros.Models.DbContext;
using FluentValidation;

namespace CrudCarros.Validators
{
    public class VeiculoValidator : AbstractValidator<Veiculo>
    {
        public VeiculoValidator()
        {
            RuleFor(v => v.Nome)
                .NotEmpty().WithMessage("O nome do veículo é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do veículo deve ter no máximo 100 caracteres.");

            RuleFor(v => v.AnoFabricacao)
                .InclusiveBetween(1900, DateTime.Now.Year).WithMessage("O ano de fabricação deve ser válido.");

            RuleFor(v => v.Preco)
                .GreaterThan(0).WithMessage("O preço deve ser um valor positivo.");

            RuleFor(v => v.FabricanteId)
                .NotEmpty().WithMessage("O fabricante é obrigatório.");

            RuleFor(v => v.Tipo)
                .IsInEnum().WithMessage("O tipo de veículo é inválido.");

            RuleFor(v => v.Descricao)
                .MaximumLength(500).WithMessage("A descrição deve ter no máximo 500 caracteres.")
                .When(v => !string.IsNullOrEmpty(v.Descricao));
        }
    }
}