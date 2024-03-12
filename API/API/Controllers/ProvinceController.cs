using Driver.API.Common;
using Driver.Common.Models;
using Driver.Domain.Entities;
using Driver.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Moneyon.Common.Data;

namespace API.Controllers;

[ApiController]
[Route("api/province")]
public class ProvinceController : ControllerBase
{
    private readonly InfoService service;
    private readonly InfoService _infoService;
    public ProvinceController(InfoService service, InfoService infoService)
    {
        this.service = service;
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
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.ProvinceView })]
    public async Task<DataResult<ProvinceShortModel>> GetProvincesPagableAsync([FromQuery] DataRequest request)
    {
        return await service.GetProvincePagableAsync(request);
    }


    [HttpGet]
    [Route("{provinceId}")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.ProvinceView })]
    public async Task<ProvinceShortModel> GetProvinceAsync(int provinceId)
    {
        return await service.GetProvinceAsync(provinceId);
    }

    [HttpPost]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.ProvinceCreate })]
    public async Task CreateProvinceAsync(ProvinceShortModel model)
    {
        await service.CreateProvinceAsync(model);
    }


    [HttpPut]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.ProvinceEdit })]
    public async Task UpdateProvinceAsync(ProvinceShortModel model)
    {
        await service.UpdateProvinceAsync(model);
    }

    [HttpDelete]
    [Route("{provinceId}")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.ProvinceDelete })]
    public async Task DeleteProvinceAsync(int provinceId)
    {
        await service.DeleteProvinceAsync(provinceId);
    }
}
