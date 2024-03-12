using Driver.API.Common;
using Driver.Common.Models;
using Driver.Domain.Entities;
using Driver.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Moneyon.Common.Data;

namespace API.Controllers;

[ApiController]
[Route("api/auto-brand")]
public class AutoBrandController : ControllerBase
{
    private readonly AutoBrandService autoBrandService;

    public AutoBrandController(AutoBrandService autoBrandService)
    {
        this.autoBrandService = autoBrandService;
    }

    [HttpGet]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[]{PermissionEnum.AutoBrandView})]
    public async Task<DataResult<AutoBrandModel>> GetBrandsPagableAsync([FromQuery] DataRequest request)
    {
        return await autoBrandService.GetBrandsPagableAsync(request);
    }

    [HttpGet]
    [Route("brands")]
    [JWTAuthorization()]
    public async Task<IEnumerable<AutoBrandModel>> GetBrandsAsync()
    {
        return await autoBrandService.GetBrandsAsync();
    }

    [HttpGet]
    [Route("{brandId}")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.AutoBrandView })]
    public async Task<AutoBrandModel> GetBrandAsync(int brandId)
    {
        return await autoBrandService.GetBrandAsync(brandId);
    }

    [HttpPost]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.AutoBrandCreate })]
    public async Task CreateBrandAsync(AutoBrandModel model)
    {
        await autoBrandService.CreateBrandAsync(model);
    }


    [HttpPut]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.AutoBrandEdit })]
    public async Task UpdateBrandAsync(AutoBrandModel model)
    {
        await autoBrandService.UpdateBrandAsync(model);
    }

    [HttpDelete]
    [Route("{brandId}")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.AutoBrandDelete })]
    public async Task DeleteBrandAsync(int brandId)
    {
        await autoBrandService.DeleteBrandAsync(brandId);
    }
}
