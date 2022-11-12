using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CepController : ControllerBase
{
    private readonly ILogger<CepController> _logger;
    private readonly IHttpClientFactory _clientFactory;

    public CepController(ILogger<CepController> logger, IHttpClientFactory clientFactory)
    {
        _logger = logger;
        _clientFactory = clientFactory;
    }

    [HttpGet]
    public async Task<IActionResult> ObterCep(string cep)
    {
        _logger.LogTrace($"Obtendo cep {cep}");

        cep = cep.Trim().Replace("-", "");

        var http = _clientFactory.CreateClient();
        var response = await http.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
        var result = await response.Content.ReadAsStringAsync();

        return Ok(result);
    }
}