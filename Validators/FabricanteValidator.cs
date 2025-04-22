using CrudCarros.Models.DbContext;
using FluentValidation;

namespace CrudCarros.Validators
{
    public class FabricanteValidator : AbstractValidator<Fabricante>
    {
        public FabricanteValidator()
        {
            RuleFor(f => f.Nome)
                .NotEmpty().WithMessage("O nome do fabricante é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do fabricante deve ter no máximo 100 caracteres.");

            RuleFor(f => f.PaisDeOrigem)
                .NotEmpty().WithMessage("O país de origem é obrigatório.")
                .MaximumLength(50).WithMessage("O país de origem deve ter no máximo 50 caracteres.");

            RuleFor(f => f.AnoDeFundacao)
                .InclusiveBetween(1800, DateTime.Now.Year).WithMessage("O ano de fundação deve ser válido e no passado.");

            RuleFor(f => f.Website)
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                .When(f => !string.IsNullOrEmpty(f.Website))
                .WithMessage("O website deve ser uma URL válida.");
        }
    }
}