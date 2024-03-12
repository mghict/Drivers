using Driver.API.Common;
using Driver.Common.Models;
using Driver.Domain.Entities;
using Driver.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Moneyon.Common.Data;

namespace API.Controllers;

[ApiController]
[Route("api/city")]
public class CityController : ControllerBase
{
    private readonly InfoService service;
    private readonly InfoService _infoService;
    public CityController(InfoService service, InfoService infoService)
    {
        this.service = service;
        _infoService = infoService;
    }

    [HttpGet]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.CityView })]
    public async Task<DataResult<CityModel>> GetCitiesPagableAsync([FromQuery] DataRequest request)
    {
        return await service.GetCityPagableAsync(request);
    }

    [HttpGet]
    [Route("{provinceId}/cities")]
    [JWTAuthorization()]
    public async Task<IEnumerable<CityShortModel>> ProvinceCities(int provinceId)
    {
        return await _infoService.GetProvinceCitiesAsync(provinceId);
    }

    [HttpGet]
    [Route("{cityId}")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.CityView })]
    public async Task<CityShortModel> GetCityAsync(int cityId)
    {
        return await service.GetCityAsync(cityId);
    }

    [HttpPost]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.CityCreate })]
    public async Task CreateCityAsync(CityShortModel model)
    {
        await service.CreateCityAsync(model);
    }


    [HttpPut]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.CityEdit })]
    public async Task UpdateCityAsync(CityShortModel model)
    {
        await service.UpdateCityAsync(model);
    }

    [HttpDelete]
    [Route("{cityId}")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.CityDelete })]
    public async Task DeleteCityAsync(int cityId)
    {
        await service.DeleteCityAsync(cityId);
    }
}
