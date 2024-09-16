using Microsoft.AspNetCore.Mvc;
using desafio_dev.API.Core.Services.Interface;

namespace desafio_dev.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrevisaoController : ControllerBase
{
    private readonly IService _service;
    public PrevisaoController(IService httpService)
    {
        _service = httpService;
    }

    [HttpGet("previsaoAtual")]
    public async Task<IActionResult> GetPrevisaoAtual([FromQuery] string cidade)
    {
        var result = await _service.GetPrevisaoAtualAsync(cidade);
        return result is null ? NoContent() : Ok(result);
    }

    [HttpGet("previsaoEstendia")]
    public async Task<IActionResult> GetPrevisaoEstendida([FromQuery] string cidade, [FromQuery] int diasPrevisao)
    {
        var result = await _service.GetPrevisaoEstendidaAsync(cidade, diasPrevisao);
        return result is null ? NoContent() : Ok(result);
    }

    [HttpGet("historicoPrevisao")]
    public async Task<IActionResult> GetHistoricoPrevisao()
    {
        var weatherHistory = await _service.GetHistoricoAsync();
        return weatherHistory.Any() ? Ok(weatherHistory) : NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCache()
    {
        var result = await _service.DeleteCache();
        return Ok(result);
    }

}

