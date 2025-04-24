using Microsoft.AspNetCore.Identity;

namespace CrudCarros.Models
{
    public class Usuario : IdentityUser
    {
        public string? Nome { get; set; }
    }
}