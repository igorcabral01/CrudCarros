using CrudCarros.Models;
using CrudCarros.Validators;
using Xunit;

namespace CrudCarros.Tests
{
    public class UsuarioValidatorTests
    {
        [Fact]
        public void Deve_Aceitar_Usuario_Valido()
        {
            var usuario = new Usuario
            {
                Nome = "Usuário Teste",
                Email = "usuario@teste.com",
                UserName = "usuario@teste.com"
            };
            var validator = new UsuarioValidator();
            var result = validator.Validate(usuario);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Deve_Rejeitar_Nome_Vazio()
        {
            var usuario = new Usuario
            {
                Nome = "",
                Email = "usuario@teste.com",
                UserName = "usuario@teste.com"
            };
            var validator = new UsuarioValidator();
            var result = validator.Validate(usuario);
            Assert.False(result.IsValid);
        }
    }
}
