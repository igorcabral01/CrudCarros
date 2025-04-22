using CrudCarros.Models;
using FluentValidation;

namespace CrudCarros.Validators
{
    public class VendaValidator : AbstractValidator<Venda>
    {
        public VendaValidator()
        {
            RuleFor(v => v.ConcessionariaId)
                .NotEmpty().WithMessage("A concessionária é obrigatória.");

            RuleFor(v => v.FabricanteId)
                .NotEmpty().WithMessage("O fabricante é obrigatório.");

            RuleFor(v => v.VeiculoId)
                .NotEmpty().WithMessage("O veículo é obrigatório.");

            RuleFor(v => v.ClienteNome)
                .NotEmpty().WithMessage("O nome do cliente é obrigatório.");

            RuleFor(v => v.ClienteCPF)
                .NotEmpty().WithMessage("O CPF do cliente é obrigatório.")
                .Matches("\\d{3}\\.\\d{3}\\.\\d{3}-\\d{2}").WithMessage("O CPF deve estar no formato 000.000.000-00.");

            RuleFor(v => v.ClienteTelefone)
                .NotEmpty().WithMessage("O telefone do cliente é obrigatório.")
                .Matches("^\\+?[1-9][0-9]{7,14}$").WithMessage("O telefone deve ser válido.");

            RuleFor(v => v.DataVenda)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("A data da venda não pode ser futura.");

            RuleFor(v => v.PrecoVenda)
                .GreaterThan(0).WithMessage("O preço de venda deve ser um valor positivo.");

            RuleFor(v => v.NumeroProtocolo)
                .NotEmpty().WithMessage("O número de protocolo é obrigatório.");
        }
    }
}