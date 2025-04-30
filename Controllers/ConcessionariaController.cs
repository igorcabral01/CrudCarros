using CrudCarros.Models;
using CrudCarros.Services;
using CrudCarros.Services.Concessionarias;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudCarros.Controllers
{
    [Route("Concessionaria")]
    public class ConcessionariaController : Controller
    {
        private readonly ConcessionariaService _concessionariaService;

        public ConcessionariaController(ConcessionariaService concessionariaService)
        {
            _concessionariaService = concessionariaService;
        }

        // --- Ações MVC (Views) ---
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var concessionarias = await _concessionariaService.ObterTodos();
            return View(concessionarias);
        }

        [HttpGet("Detalhes/{id}")]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            var concessionaria = await _concessionariaService.ObterEntidadePorId(id);
            if (concessionaria == null)
                return View("ConcessionariaNaoEncontrada");
            return View(concessionaria);
        }

        [HttpGet("Editar/{id}")]
        public async Task<IActionResult> Editar(Guid id)
        {
            var concessionaria = await _concessionariaService.ObterEntidadePorId(id);
            if (concessionaria == null)
                return NotFound();
            return View(concessionaria);
        }

        [HttpPost("Editar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Guid id, Concessionaria concessionaria)
        {
            if (!ModelState.IsValid)
                return View(concessionaria);
            if (id != concessionaria.ConcessionariaId)
                return BadRequest();
            await _concessionariaService.Atualizar(concessionaria);
            return RedirectToAction("Detalhes", new { id = concessionaria.ConcessionariaId });
        }

        [HttpPost("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir(Guid id)
        {
            await _concessionariaService.Excluir(id);
            return RedirectToAction("Index", "Home");
        }

        // --- Ações API REST ---
        [HttpGet]
        [Route("/api/Concessionaria")]
        public async Task<IActionResult> GetAll()
        {
            var concessionarias = await _concessionariaService.ObterTodos();
            return Ok(concessionarias);
        }

        [HttpGet]
        [Route("/api/Concessionaria/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var concessionaria = await _concessionariaService.ObterPorId(id);
            if (concessionaria == null)
                return NotFound();
            return Ok(concessionaria);
        }

        [HttpPost]
        [Route("/api/Concessionaria")]
        public async Task<IActionResult> Post([FromBody] CrudCarros.Services.Concessionarias.ConcessionariaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var concessionaria = await _concessionariaService.Inserir(dto);
            return Ok(concessionaria);
        }
    }
}