using CrudCarros.Models;
using CrudCarros.Validators;
using Xunit;
using System;

namespace CrudCarros.Tests
{
    public class VendaValidatorTests
    {
        [Fact]
        public void Deve_Aceitar_Venda_Valida()
        {
            var venda = new Venda
            {
                ConcessionariaId = 1,
                FabricanteId = 1,
                VeiculoId = 1,
                ClienteNome = "João Silva",
                ClienteCPF = "123.456.789-00",
                ClienteTelefone = "11999999999", // formato aceito pelo Validator
                DataVenda = DateTime.Now.AddDays(-1),
                PrecoVenda = 80000,
                NumeroProtocolo = "1234567890"
            };
            var validator = new VendaValidator();
            var result = validator.Validate(venda);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Deve_Rejeitar_DataVenda_Futura()
        {
            var venda = new Venda
            {
                ConcessionariaId = 1,
                FabricanteId = 1,
                VeiculoId = 1,
                ClienteNome = "João Silva",
                ClienteCPF = "123.456.789-00",
                ClienteTelefone = "(11) 99999-9999",
                DataVenda = DateTime.Now.AddDays(1),
                PrecoVenda = 80000,
                NumeroProtocolo = "1234567890"
            };
            var validator = new VendaValidator();
            var result = validator.Validate(venda);
            Assert.False(result.IsValid);
        }
    }
}
