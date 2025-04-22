using CrudCarros.Models;
using FluentValidation;

namespace CrudCarros.Validators
{
    public class ConcessionariaValidator : AbstractValidator<Concessionaria>
    {
        public ConcessionariaValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome da concessionária é obrigatório.")
                .MaximumLength(100).WithMessage("O nome da concessionária deve ter no máximo 100 caracteres.");

            RuleFor(c => c.Rua).NotEmpty().WithMessage("A rua é obrigatória.");
            RuleFor(c => c.Cidade).NotEmpty().WithMessage("A cidade é obrigatória.");
            RuleFor(c => c.Estado).NotEmpty().WithMessage("O estado é obrigatório.");

            RuleFor(c => c.CEP)
                .NotEmpty().WithMessage("O CEP é obrigatório.")
                .Matches("\\d{5}-\\d{3}").WithMessage("O CEP deve estar no formato 00000-000.");

            RuleFor(c => c.Telefone)
                .NotEmpty().WithMessage("O telefone é obrigatório.")
                .Matches("^\\+?[1-9][0-9]{7,14}$").WithMessage("O telefone deve ser válido.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("O e-mail deve ser válido.");

            RuleFor(c => c.CapacidadeMaximaVeiculos)
                .GreaterThan(0).WithMessage("A capacidade máxima deve ser um valor positivo.");
        }
    }
}