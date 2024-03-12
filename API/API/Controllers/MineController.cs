using Driver.API.Common;
using Driver.Common.Models;
using Driver.Domain.Entities;
using Driver.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Moneyon.Common.Data;

namespace API.Controllers;

[ApiController]
[Route("api/mine")]
public class MineController : ControllerBase
{
    private readonly MineService _mineService;

    public MineController(MineService mineService)
    {
        _mineService = mineService;
    }

    [HttpGet]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.MineView })]
    public async Task<DataResult<MineShortModel>> GetMineList([FromQuery] DataRequest request)
    {
        return await _mineService.GetMineListAsync(request);
    }

    [HttpGet]
    [Route("mines")]
    [JWTAuthorization()]
    public async Task<IEnumerable<MineShortModel>> GetList()
    {
        return await _mineService.GetListAsync();
    }

    [HttpGet]
    [Route("{id}")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.MineView })]
    public async Task<MineModel> GetMineItem(long id)
    {
        return await _mineService.GetMineAsync(id);
    }

    [HttpPost]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum. MineCreate})]
    public async Task CreateMine(MineCreateModel model)
    {
        await _mineService.CreateMineAsync(model);
    }

    [HttpPut]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.MineEdit })]
    public async Task UpdateMine(MineUpdateModel model)
    {
        await _mineService.UpdateMineAsync(model);
    }

    [HttpDelete]
    [Route("{mineId}")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.MineDelete })]
    public async Task DeleteMine(long mineId)
    {
        await _mineService.DeleteMineAsync(mineId);
    }

}
