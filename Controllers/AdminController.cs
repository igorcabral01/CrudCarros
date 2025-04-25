using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CrudCarros.Models;

namespace CrudCarros.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdminController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Usuarios()
        {
            var usuarios = _userManager.Users.ToList();
            var model = new List<UsuarioComRolesViewModel>();
            foreach (var usuario in usuarios)
            {
                var roles = await _userManager.GetRolesAsync(usuario);
                model.Add(new UsuarioComRolesViewModel
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Roles = roles.ToList()
                });
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PromoverParaAdmin(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario != null)
            {
                await _userManager.AddToRoleAsync(usuario, "Administrador");
            }
            return RedirectToAction("Usuarios");
        }

        [HttpPost]
        public async Task<IActionResult> RebaixarParaUsuario(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario != null)
            {
                await _userManager.RemoveFromRoleAsync(usuario, "Administrador");
                await _userManager.AddToRoleAsync(usuario, "Usuario");
            }
            return RedirectToAction("Usuarios");
        }
    }

    public class UsuarioComRolesViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
