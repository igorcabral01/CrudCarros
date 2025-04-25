using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CrudCarros.Services
{
    public class CepService
    {
        private readonly HttpClient _httpClient;

        public CepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<dynamic> ConsultarCepAsync(string cep)
        {
            var response = await _httpClient.GetAsync($"https://brasilapi.com.br/api/cep/v1/{cep}");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Erro ao consultar o CEP: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<dynamic>(content) ?? throw new InvalidOperationException("A resposta da API de CEP é nula.");
        }
    }
}