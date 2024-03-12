using Driver.API.Common;
using Driver.Common.Models;
using Driver.Domain.Entities;
using Driver.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Moneyon.Common.Data;

namespace API.Controllers;

[ApiController]
[Route("api/material")]
public class MaterialController : ControllerBase
{
    private readonly MaterialService service;

    public MaterialController(MaterialService service)
    {
        this.service = service;
    }

    [HttpGet]
    [Route("materials")]
    public async Task<IEnumerable<MaterialModel>> GetMaterialListAsync()
    {
        return await service.GetMaterialsAsync();
    }

    [HttpGet]
    [Route("{mineId}/materials")]
    public async Task<IEnumerable<MaterialModel>> GetMineMaterialListAsync(long mineId)
    {
        return await service.GetMaterialsByMineAsync(mineId);
    }

    [HttpGet]
    [Route("mine/{mineCode}/materials")]
    public async Task<IEnumerable<MaterialModel>> GetMineMaterialsListAsync(long mineCode)
    {
        return await service.GetMaterialsByMineCodeAsync(mineCode);
    }

    [HttpGet]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.MaterialView })]
    public async Task<DataResult<MaterialModel>> GetMaterialsPagableAsync([FromQuery] DataRequest request)
    {
        return await service.GetMaterialPagableAsync(request);
    }


    [HttpGet]
    [Route("{materialId}")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.MaterialView })]
    public async Task<MaterialModel> GetMaterialAsync(int materialId)
    {
        return await service.GetMaterialAsync(materialId);
    }

    [HttpPost]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.MaterialCreate })]
    public async Task CreateMaterialAsync(MaterialModel model)
    {
        await service.CreateMaterialAsync(model);
    }


    [HttpPut]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.MaterialEdit })]
    public async Task UpdateMaterialAsync(MaterialModel model)
    {
        await service.UpdateMaterialAsync(model);
    }

    [HttpDelete]
    [Route("{materialId}")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.MaterialDelete })]
    public async Task DeleteMaterialAsync(int materialId)
    {
        await service.DeleteMaterialAsync(materialId);
    }
}
