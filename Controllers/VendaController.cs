using CrudCarros.Models;
using CrudCarros.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CrudCarros.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendaController : ControllerBase
    {
        private readonly VendaService _vendaService;
        private readonly IMemoryCache _memoryCache;

        public VendaController(VendaService vendaService, IMemoryCache memoryCache)
        {
            _vendaService = vendaService;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            string cacheKey = "Vendas";
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<Venda>? vendas))
            {
                vendas = await _vendaService.ObterTodos();
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
                _memoryCache.Set(cacheKey, vendas, cacheEntryOptions);
            }
            return Ok(vendas);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(Guid id)
        {
            var venda = _vendaService.ObterPorId(id);
            if (venda == null)
            {
                return NotFound();
            }
            return Ok(venda);
        }

        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] Venda venda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _vendaService.Inserir(venda);
            return Ok(venda.VendaId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] Venda venda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != venda.VendaId)
            {
                return BadRequest("ID mismatch.");
            }

            await _vendaService.Atualizar(venda);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var venda = _vendaService.ObterPorId(id);
            if (venda == null)
            {
                return NotFound();
            }

            await _vendaService.Excluir(id);
            return NoContent();
        }
    }
}