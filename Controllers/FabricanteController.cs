using CrudCarros.Models.DbContext;
using CrudCarros.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CrudCarros.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FabricanteController : ControllerBase
    {
        private readonly FabricanteService _fabricanteService;

        public FabricanteController(FabricanteService fabricanteService)
        {
            _fabricanteService = fabricanteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var fabricantes = await _fabricanteService.GetAll();
            return Ok(fabricantes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var fabricante = await _fabricanteService.GetById(id);
            if (fabricante == null)
            {
                return NotFound();
            }
            return Ok(fabricante);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Fabricante fabricante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _fabricanteService.Add(fabricante);
            return CreatedAtAction(nameof(GetById), new { id = fabricante.FabricanteId }, fabricante);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Fabricante fabricante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fabricante.FabricanteId)
            {
                return BadRequest("ID mismatch.");
            }

            await _fabricanteService.Update(fabricante);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var fabricante = await _fabricanteService.GetById(id);
            if (fabricante == null)
            {
                return NotFound();
            }

            await _fabricanteService.Delete(id);
            return NoContent();
        }
    }
}