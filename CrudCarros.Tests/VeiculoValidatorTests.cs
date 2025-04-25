using CrudCarros.Models.DbContext;
using CrudCarros.Validators;
using Xunit;
using System;

namespace CrudCarros.Tests
{
    public class VeiculoValidatorTests
    {
        [Fact]
        public void Deve_Aceitar_Veiculo_Valido()
        {
            var veiculo = new Veiculo
            {
                Nome = "Corolla",
                AnoFabricacao = 2020,
                Preco = 90000,
                FabricanteId = 1,
                Tipo = TipoVeiculo.Carro,
                Descricao = "Sedan completo."
            };
            var validator = new VeiculoValidator();
            var result = validator.Validate(veiculo);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Deve_Rejeitar_Preco_Negativo()
        {
            var veiculo = new Veiculo
            {
                Nome = "Corolla",
                AnoFabricacao = 2020,
                Preco = -1,
                FabricanteId = 1,
                Tipo = TipoVeiculo.Carro
            };
            var validator = new VeiculoValidator();
            var result = validator.Validate(veiculo);
            Assert.False(result.IsValid);
        }
    }
}
