using CrudCarros.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CrudCarros.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CepController : ControllerBase
    {
        private readonly CepService _cepService;
        public CepController(CepService cepService)
        {
            _cepService = cepService;
        }

        [HttpGet("{cep}")]
        public async Task<IActionResult> BuscarCep(string cep)
        {
            try
            {
                var resultado = await _cepService.ConsultarCepAsync(cep);
                return Ok(resultado);
            }
            catch (System.Exception ex)
            {
                return NotFound(new { erro = ex.Message });
            }
        }
    }
}