using CrudCarros.Models;
using CrudCarros.Models.DbContext;
using CrudCarros.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CrudCarros.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrador")]
    public class VeiculoController : ControllerBase
    {
        private readonly VeiculoService _veiculoService;
        private readonly IMemoryCache _memoryCache;

        public VeiculoController(VeiculoService veiculoService, IMemoryCache memoryCache)
        {
            _veiculoService = veiculoService;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            string cacheKey = "Veiculos";
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<Veiculo>? veiculos))
            {
                veiculos = await _veiculoService.ObterTodos();
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
                _memoryCache.Set(cacheKey, veiculos, cacheEntryOptions);
            }
            return Ok(veiculos);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(Guid id)
        {
            var veiculo = _veiculoService.ObterPorId(id);
            if (veiculo == null)
            {
                return NotFound();
            }
            return Ok(veiculo);
        }

        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] Veiculo veiculo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _veiculoService.Inserir(veiculo);
            return Ok(new { sucesso = true, nome = veiculo.Nome });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] Veiculo veiculo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != veiculo.VeiculoId)
            {
                return BadRequest("ID mismatch.");
            }

            await _veiculoService.Atualizar(veiculo);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var veiculo = _veiculoService.ObterPorId(id);
            if (veiculo == null)
            {
                return NotFound();
            }

            await _veiculoService.Excluir(id);
            return NoContent();
        }
    }
}