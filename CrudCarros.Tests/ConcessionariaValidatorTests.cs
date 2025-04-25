using CrudCarros.Models;
using CrudCarros.Validators;
using Xunit;
using System;

namespace CrudCarros.Tests
{
    public class ConcessionariaValidatorTests
    {
        [Fact]
        public void Deve_Aceitar_Concessionaria_Valida()
        {
            var concessionaria = new Concessionaria
            {
                Nome = "Concessionária Exemplo",
                Rua = "Rua Exemplo",
                Cidade = "Cidade Exemplo",
                Estado = "SP",
                CEP = "12345-678",
                Telefone = "11999999999", // formato aceito pelo Validator
                Email = "exemplo@concessionaria.com",
                CapacidadeMaximaVeiculos = 100
            };
            var validator = new ConcessionariaValidator();
            var result = validator.Validate(concessionaria);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Deve_Rejeitar_CEP_Invalido()
        {
            var concessionaria = new Concessionaria
            {
                Nome = "Concessionária Exemplo",
                Rua = "Rua Exemplo",
                Cidade = "Cidade Exemplo",
                Estado = "SP",
                CEP = "12345678",
                Telefone = "(11) 99999-9999",
                Email = "exemplo@concessionaria.com",
                CapacidadeMaximaVeiculos = 100
            };
            var validator = new ConcessionariaValidator();
            var result = validator.Validate(concessionaria);
            Assert.False(result.IsValid);
        }
    }
}
