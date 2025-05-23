using CrudCarros.Models;
using CrudCarros.Services.Vendas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrudCarros.Controllers
{
    [Route("Venda")]
    public class VendaController : Controller
    {
        private readonly VendaService _vendaService;

        public VendaController(VendaService vendaService)
        {
            _vendaService = vendaService;
        }

        [HttpPost("RealizarVenda")]
        public async Task<IActionResult> RealizarVenda([FromBody] Venda venda)
        {
            try
            {
                var protocolo = await _vendaService.RealizarVendaAsync(venda);
                return Ok(new { Protocolo = protocolo });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Erro = ex.Message });
            }
        }

        [HttpGet("Concessionarias")]
        public async Task<IActionResult> BuscarConcessionarias(string termo)
        {
            var concessionarias = await _vendaService.BuscarConcessionariasAsync(termo);
            return Ok(concessionarias);
        }

        // [HttpGet("Fabricantes")]
        // public async Task<IActionResult> BuscarFabricantes(string termo)
        // {
        //     var fabricantes = await _vendaService.BuscarFabricantesAsync(termo);
        //     return Ok(fabricantes);
        // }

        [HttpGet("Veiculos")]
        public async Task<IActionResult> BuscarVeiculos(string fabricante, string modelo)
        {
            var veiculos = await _vendaService.BuscarVeiculosAsync(fabricante, modelo);
            return Ok(veiculos);
        }

        [HttpGet("VeiculosPorFabricante")]
        public async Task<IActionResult> VeiculosPorFabricante(string fabricanteId)
        {
            if (string.IsNullOrEmpty(fabricanteId) || !Guid.TryParse(fabricanteId, out var guid))
                return BadRequest(new { Erro = "FabricanteId inválido." });
            var veiculos = await _vendaService.BuscarVeiculosPorFabricanteAsync(guid);
            return Ok(veiculos);
        }

        [HttpGet("RealizarVenda")]
        public IActionResult RealizarVenda()
        {
            return View();
        }
    }
}