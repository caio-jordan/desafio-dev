using Microsoft.AspNetCore.Mvc;
using desafio_dev.API.Core.Services.Interface;
using System.Diagnostics.CodeAnalysis;

namespace desafio_dev.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ExcludeFromCodeCoverage]
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

    [HttpGet("cachePrevisao")]
    public async Task<IActionResult> GetCachePrevisao()
    {
        var weathercache = await _service.GetCacheAsync();
        return weathercache.Any() ? Ok(weathercache) : NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCache()
    {
        var result = await _service.DeleteCacheAsync();
        return Ok(result);
    }

}

