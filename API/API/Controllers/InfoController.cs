using Driver.API.Common;
using Driver.Common.Models;
using Driver.Domain.Entities;
using Driver.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/info")]
public class InfoController : ControllerBase
{
    private readonly InfoService _infoService;

    public InfoController(InfoService infoService)
    {
        _infoService = infoService;
    }

    [HttpGet]
    [Route("provinces")]
    [JWTAuthorization()]
    public async Task<IEnumerable<ProvinceShortModel>> Provinces()
    {
        return await _infoService.GetProvincesAsync();
    }

    [HttpGet]
    [Route("{provinceId}/cities")]
    [JWTAuthorization()]
    public async Task<IEnumerable<CityShortModel>> ProvinceCities(int provinceId)
    {
        return await _infoService.GetProvinceCitiesAsync(provinceId);
    }
}
