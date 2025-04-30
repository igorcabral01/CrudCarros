using CrudCarros.Models;
using CrudCarros.Models.DbContext;
using CrudCarros.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CrudCarros.Controllers
{
    [Route("[controller]")]
    public class FabricanteController : Controller
    {
        private readonly FabricanteService _fabricanteService;
        private readonly IMemoryCache _memoryCache;

        public FabricanteController(FabricanteService fabricanteService, IMemoryCache memoryCache)
        {
            _fabricanteService = fabricanteService;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            string cacheKey = "Fabricantes";
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<Fabricante>? fabricantes))
            {
                fabricantes = await _fabricanteService.GetAll();
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
                _memoryCache.Set(cacheKey, fabricantes, cacheEntryOptions);
            }
            return Ok(fabricantes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var fabricante = await _fabricanteService.GetById(id);
            if (fabricante == null)
            {
                return NotFound();
            }
            return Ok(fabricante);
        }

        [HttpGet("Inserir")]
        public async Task<IActionResult> Inserir()
        {
            var fabricantes = await _fabricanteService.GetAll();
            ViewBag.Fabricantes = fabricantes;
            return View();
        }

        [HttpPost("Inserir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Inserir(Fabricante fabricante)
        {
            if (!ModelState.IsValid)
            {
                var fabricantes = await _fabricanteService.GetAll();
                ViewBag.Fabricantes = fabricantes;
                ViewBag.MensagemErro = "Preencha todos os campos obrigatórios corretamente.";
                return View(fabricante);
            }
            await _fabricanteService.Add(fabricante);
            ViewBag.MensagemSucesso = "Fabricante cadastrado com sucesso!";
            ModelState.Clear();
            var lista = await _fabricanteService.GetAll();
            ViewBag.Fabricantes = lista;
            return View();
        }

        [HttpGet("Editar/{id}")]
        public async Task<IActionResult> Editar(Guid id)
        {
            var fabricante = await _fabricanteService.GetById(id);
            if (fabricante == null)
                return NotFound();
            return View(fabricante);
        }

        [HttpPost("Editar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Guid id, Fabricante fabricante)
        {
            if (!ModelState.IsValid)
                return View(fabricante);
            if (id != fabricante.FabricanteId)
                return BadRequest();
            await _fabricanteService.Update(fabricante);
            return RedirectToAction("Inserir");
        }

        [HttpPost("Atualizar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] Fabricante fabricante)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (id != fabricante.FabricanteId)
                return BadRequest("ID mismatch.");
            await _fabricanteService.Update(fabricante);
            return Ok();
        }

      
        [HttpPost("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var fabricante = await _fabricanteService.GetById(id);
            if (fabricante == null)
            {
                return NotFound();
            }
            await _fabricanteService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var fabricantes = await _fabricanteService.GetAll();
            return View(fabricantes);
        }
    }
}