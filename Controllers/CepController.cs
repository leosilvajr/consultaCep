using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsultaCepApi.Controllers
{
    [Route("Cep")]
    [ApiController]
    public class CepController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CepController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("{cep}")]
        public async Task<IActionResult> Get(string cep)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }
    }
}
