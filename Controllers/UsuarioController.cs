using CrudCarros.Models;
using CrudCarros.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Identity;

namespace CrudCarros.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly IMemoryCache _memoryCache;
        private readonly UserManager<Usuario> _userManager;

        public UsuarioController(UsuarioService usuarioService, IMemoryCache memoryCache, UserManager<Usuario> userManager)
        {
            _usuarioService = usuarioService;
            _memoryCache = memoryCache;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            string cacheKey = "Usuarios";
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<Usuario>? usuarios))
            {
                usuarios = await _usuarioService.ObterTodos();
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
                _memoryCache.Set(cacheKey, usuarios, cacheEntryOptions);
            }
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(Guid id)
        {
            var usuario = _usuarioService.ObterPorId(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _usuarioService.Inserir(usuario);
            return Ok(usuario.Nome);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Guid.Parse(usuario.Id))
            {
                return BadRequest("ID mismatch.");
            }

            await _usuarioService.Atualizar(usuario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var usuario = _usuarioService.ObterPorId(id);
            if (usuario == null)
            {
                return NotFound();
            }
            await _usuarioService.Excluir(id);
            return NoContent();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var password = usuario.PasswordHash; // Supondo que a senha seja passada no campo PasswordHash
            usuario.PasswordHash = null; // Limpa o campo para evitar problemas de segurança

            var result = await _userManager.CreateAsync(usuario, password);

            if (result.Succeeded)
            {
                return Ok("Usuário registrado com sucesso.");
            }

            return BadRequest(result.Errors);
        }
    }
}