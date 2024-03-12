using Driver.API.Common;
using Driver.Common.Models;
using Driver.Domain.Entities;
using Driver.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Moneyon.Common.Data;

namespace API.Controllers;

[ApiController]
[Route("api/driver")]
public class DriverController : ControllerBase
{
    private readonly DriverService driverService;

    public DriverController(DriverService driverService)
    {
        this.driverService = driverService;
    }

    [HttpGet]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.DriverView })]
    public async Task<DataResult<PersonModel>> GetDriversPagableAsync([FromQuery] DataRequest request)
    {
        return await driverService.GetDriversPagableAsync(request);
    }

    [HttpGet]
    [Route("drivers")]
    [JWTAuthorization()]
    public async Task<IEnumerable<PersonShortModel>> GetDriversAsync()
    {
        return await driverService.GetDriversAsync();
    }

    [HttpGet]
    [Route("{personCode}")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.DriverView })]
    public async Task<PersonModel> GetDriverAsync(Guid personCode)
    {
        var driver= await driverService.GetDriverAsync(personCode);
        driver?.Document?.SetUserScopeUrls(Url);

        return driver;
    }

    [HttpPost]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.DriverCreate })]
    public async Task CreateDriverAsync(PersonCreateModel model)
    {
        await driverService.CreateDriverAsync(model);
    }


    [HttpPut]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.DriverEdit })]
    public async Task UpdateDriverAsync(PersonUpdateModel model)
    {
        await driverService.UpdateDriverAsync(model);
    }

    [HttpDelete]
    [Route("{personCode}")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.DriverDelete })]
    public async Task DeleteDriverAsync(Guid personCode)
    {
        await driverService.DeleteDriverAsync(personCode);
    }

}
