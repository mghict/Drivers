using Driver.API.Common;
using Driver.Common.Models;
using Driver.Domain.Entities;
using Driver.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Moneyon.Common.Data;

namespace API.Controllers;

[ApiController]
[Route("api/auto-model")]
public class AutoModelController : ControllerBase
{
    private readonly AutoModelService autoModelService;

    public AutoModelController(AutoModelService autoModelService)
    {
        this.autoModelService = autoModelService;
    }

    [HttpGet]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.AutoModelView })]
    public async Task<DataResult<AutoModelsModel>> GetModelsPagableAsync([FromQuery] DataRequest request)
    {
        return await autoModelService.GetModelsPagableAsync(request);
    }

    [HttpGet]
    [Route("{brandId}/models")]
    [JWTAuthorization()]
    public async Task<IEnumerable<AutoModelsModel>> GetModelsAsync(int brandId)
    {
        return await autoModelService.GetModelsAsync(brandId);
    }



    [HttpGet]
    [Route("{modelId}")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.AutoModelView })]
    public async Task<AutoModelsModel> GetModelAsync(int modelId)
    {
        return await autoModelService.GetModelAsync(modelId);
    }

    [HttpPost]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.AutoModelCreate })]
    public async Task CreateModelAsync(AutoModelsModel model)
    {
        await autoModelService.CreateModelAsync(model);
    }


    [HttpPut]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.AutoModelEdit })]
    public async Task UpdateModelAsync(AutoModelsModel model)
    {
        await autoModelService.UpdateModelAsync(model);
    }

    [HttpDelete]
    [Route("{modelId}")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.AutoModelDelete })]
    public async Task DeleteModelAsync(int modelId)
    {
        await autoModelService.DeleteModelAsync(modelId);
    }
}
