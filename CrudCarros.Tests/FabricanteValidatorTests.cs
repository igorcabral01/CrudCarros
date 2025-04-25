using CrudCarros.Models.DbContext;
using CrudCarros.Validators;
using Xunit;
using System;

namespace CrudCarros.Tests
{
    public class FabricanteValidatorTests
    {
        [Fact]
        public void Deve_Aceitar_Fabricante_Valido()
        {
            var fabricante = new Fabricante
            {
                Nome = "Toyota",
                PaisDeOrigem = "Japão",
                AnoDeFundacao = 1937,
                Website = "https://www.toyota.com"
            };
            var validator = new FabricanteValidator();
            var result = validator.Validate(fabricante);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Deve_Rejeitar_AnoDeFundacao_Futuro()
        {
            var fabricante = new Fabricante
            {
                Nome = "Toyota",
                PaisDeOrigem = "Japão",
                AnoDeFundacao = DateTime.Now.Year + 1,
                Website = "https://www.toyota.com"
            };
            var validator = new FabricanteValidator();
            var result = validator.Validate(fabricante);
            Assert.False(result.IsValid);
        }
    }
}
