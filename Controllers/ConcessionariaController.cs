using CrudCarros.Models;
using CrudCarros.Services;
using CrudCarros.Services.Concessionarias;
using CrudCarros.Services.Vendas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.IO;

namespace CrudCarros.Controllers
{
    [Route("Concessionaria")]
    public class ConcessionariaController : Controller
    {
        private readonly ConcessionariaService _concessionariaService;
        private readonly VendaService _vendaService;

        public ConcessionariaController(ConcessionariaService concessionariaService, VendaService vendaService)
        {
            _concessionariaService = concessionariaService;
            _vendaService = vendaService;
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

        [HttpGet("RelatorioVendasPdf/{id}")]
        public async Task<IActionResult> RelatorioVendasPdf(Guid id)
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            var concessionaria = await _concessionariaService.ObterEntidadePorId(id);
            if (concessionaria == null)
                return NotFound();
            var vendas = await _vendaService.ObterVendasPorConcessionariaId(id);

            var stream = new MemoryStream();
            var doc = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(x => x.FontFamily("Arial").FontSize(12));
                    page.Header().Row(row =>
                    {
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Text($"Relatório de Vendas - {concessionaria.Nome}")
                                .FontSize(20).Bold().FontColor("#1a237e");
                            col.Item().Text($"Cidade: {concessionaria.Cidade} - {concessionaria.Estado}")
                                .FontSize(12).FontColor("#424242");
                        });
                    });
                    page.Content().PaddingVertical(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2); // Cliente
                            columns.RelativeColumn(2); // Veículo
                            columns.RelativeColumn(2); // Fabricante
                            columns.RelativeColumn(); // Data
                            columns.RelativeColumn(); // Preço
                            columns.RelativeColumn(2); // Protocolo
                        });
                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("Cliente").Bold();
                            header.Cell().Element(CellStyle).Text("Veículo").Bold();
                            header.Cell().Element(CellStyle).Text("Fabricante").Bold();
                            header.Cell().Element(CellStyle).Text("Data").Bold();
                            header.Cell().Element(CellStyle).Text("Preço").Bold();
                            header.Cell().Element(CellStyle).Text("Protocolo").Bold();
                        });
                        foreach (var venda in vendas)
                        {
                            table.Cell().Element(CellStyle).Text(venda.ClienteNome);
                            table.Cell().Element(CellStyle).Text(venda.veiculo?.Nome ?? "");
                            table.Cell().Element(CellStyle).Text(venda.Fabricante?.Nome ?? "");
                            table.Cell().Element(CellStyle).Text(venda.DataVenda.ToString("dd/MM/yyyy"));
                            table.Cell().Element(CellStyle).Text($"R$ {venda.PrecoVenda:N2}");
                            table.Cell().Element(CellStyle).Text(venda.NumeroProtocolo);
                        }
                    });
                    page.Footer().AlignCenter().Text($"Gerado em {DateTime.Now:dd/MM/yyyy HH:mm}").FontSize(10).FontColor("#616161");
                });
            });
            doc.GeneratePdf(stream);
            stream.Position = 0;
            return File(stream, "application/pdf", $"RelatorioVendas_{concessionaria.Nome}.pdf");

            IContainer CellStyle(IContainer container) =>
                container.PaddingVertical(4).PaddingHorizontal(2).Background("#fff").BorderBottom(1).BorderColor("#e0e0e0");
        }

        [HttpGet("VendasPorMes/{id}")]
        public async Task<IActionResult> VendasPorMes(Guid id)
        {
            var vendas = await _vendaService.ObterVendasPorConcessionariaId(id);
            var vendasPorMes = vendas
                .GroupBy(v => new { v.DataVenda.Year, v.DataVenda.Month })
                .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                .Select(g => new {
                    MesAno = $"{g.Key.Month:D2}/{g.Key.Year}",
                    Quantidade = g.Count()
                }).ToList();
            var labels = vendasPorMes.Select(x => x.MesAno).ToArray();
            var valores = vendasPorMes.Select(x => x.Quantidade).ToArray();
            return Json(new { labels, valores });
        }
    }
}