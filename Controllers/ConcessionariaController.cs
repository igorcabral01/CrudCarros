using CrudCarros.Models;
using CrudCarros.Services;
using CrudCarros.Services.Concessionarias;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CrudCarros.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConcessionariaController : ControllerBase
    {
        private readonly ConcessionariaService _concessionariaService;
        private readonly IMemoryCache _memoryCache;

        public ConcessionariaController(ConcessionariaService concessionariaService, IMemoryCache memoryCache)
        {
            _concessionariaService = concessionariaService;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            string cacheKey = "Concessionarias";
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<Concessionaria>? concessionarias) || concessionarias == null)
            {
                var concessionariaDtos = await _concessionariaService.ObterTodos();
                concessionarias = concessionariaDtos.Select(dto => new Concessionaria
                {
                    Nome = dto.Nome,
                    Rua = dto.Rua,
                    Cidade = dto.Cidade,
                    Estado = dto.Estado,
                    CEP = dto.CEP,
                    Telefone = dto.Telefone,
                    Email = dto.Email,
                    CapacidadeMaximaVeiculos = dto.CapacidadeMaximaVeiculos,
                    
                });
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
                _memoryCache.Set(cacheKey, concessionarias, cacheEntryOptions);
            }
            return Ok(concessionarias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var concessionaria = await _concessionariaService.ObterPorId(id);
            if (concessionaria == null)
            {
                return NotFound();
            }
            return Ok(concessionaria);
        }

        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] ConcessionariaDto concessionaria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _concessionariaService.Inserir(concessionaria);
            return Ok(concessionaria.Nome);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] Concessionaria concessionaria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != concessionaria.ConcessionariaId)
            {
                return BadRequest("ID mismatch.");
            }

            await _concessionariaService.Atualizar(concessionaria);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var concessionaria = await _concessionariaService.ObterPorId(id);
            if (concessionaria == null)
            {
                return NotFound();
            }

            await _concessionariaService.Excluir(id);
            return NoContent();
        }
    }
}