using Driver.API.Common;
using Driver.API.Controllers;
using Driver.Common.Models;
using Driver.Domain.Entities;
using Driver.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Moneyon.Common.Data;

namespace API.Controllers;

[ApiController]
[Route("api/auto")]
public class AutoController : AppBaseController
{
    private readonly AutoService autoService;

    public AutoController(IHttpContextAccessor accessor, AutoService autoService)
        :base(accessor)
    {
        this.autoService = autoService;
    }

    [HttpGet]
    [Route("{deviceCode}/auto-details")]
    public async Task<AutoDetailsDto?> GetAutoDetailsAsync(long deviceCode)
    {
        return await autoService.GetAutoDetailsAsync(deviceCode);
    }

    [HttpGet]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.AutoView })]
    public async Task<DataResult<AutoDto>> GetAutosPagableAsync([FromQuery] DataRequest request)
    {
        return await autoService.GetAutosPagableAsync(request);
    }


    [HttpGet]
    [Route("{autoId}")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.AutoBrandView })]
    public async Task<AutoDto> GetAutoAsync(long autoId)
    {
        var auto= await autoService.GetAutoAsync(autoId);
        auto?.Document?.SetUserScopeUrls(Url);

        return auto;
    }

    [HttpPost]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.AutoCreate })]
    public async Task CreateAutoAsync(AutoCreateModel model)
    {
        await autoService.CreateAutoAsync(model);
    }


    [HttpPut]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.AutoEdit })]
    public async Task UpdateAutoAsync(AutoUpdateModel model)
    {
        await autoService.UpdateAutoAsync(model);
    }

    [HttpDelete]
    [Route("{autoId}")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.AutoDelete })]
    public async Task DeleteAutoAsync(long autoId)
    {
        await autoService.DeleteAutoAsync(autoId);
    }

    [HttpGet]
    [Route("autos-history")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.AutoHistoryReportView })]
    public async Task<DataResult<AutoMissionsModel>> AutosHistoryAsync([FromQuery] DataRequest request)
    {
        CheckUser();
        CheckedDataRequest(request);
        return await autoService.GetAutosMissionsPagableAsync(request,User!);
    }

    //[HttpGet]
    //[Route("{autoHistoryId}/autos-history-details")]
    //[JWTAuthorization(new PermissionEnum[] { PermissionEnum.AutoHistoryReportView })]
    //public async Task<DataResult<AutoTransportModel>> AutosHistoryDetailsAsync([FromQuery] DataRequest request,long autoHistoryId)
    //{
    //    return await autoService.GetAutosTransportPagableAsync(request, autoHistoryId);
    //}

    [HttpGet]
    [Route("{autoHistoryId}/autos-history-details")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.AutoHistoryReportView })]
    public async Task<IEnumerable<AutoTransportModel>> AutosHistoryDetailsAsync( long autoHistoryId)
    {
        return await autoService.GetAutosTransportListAsync(autoHistoryId);
    }



    [HttpGet]
    [Route("autos-list")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.AutoHistoryReportView,PermissionEnum.AutoStatusReportView })]
    public async Task<DataResult<AutoDto>> GetAutosReportPagableAsync([FromQuery] DataRequest request)
    {
        CheckUser();
        return await autoService.GetAutosPagableAsync(request,User!);
    }

    [HttpGet]
    [Route("{autoId}/auto-online")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.AutoStatusReportView })]
    public async Task<AutoLastLocationModel> GetAutoLastLocationAsync(long autoId)
    {
        CheckUser();
        return await autoService.GetAutoLastLocationAsync(autoId);
    }


    [HttpGet]
    [Route("{autoId}/auto-errors")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.AutoStatusReportView })]
    public async Task<DataResult<AutoErrorDto>> GetAutoErrorsReportPagableAsync([FromQuery] DataRequest request,long autoId)
    {
        return await autoService.GetAutoErrorsPagableAsync(request, autoId);
    }

    [HttpGet]
    [Route("auto-offdevice")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.AutoOffDeviceReportView })]
    public async Task<DataResult<AutoOffDeviceDto>> GetAutosOffDeviceReportPagableAsync([FromQuery] DataRequest request)
    {
        CheckUser();
        return await autoService.GetAutosWithError_OFF_DevicePagableAsync(request,User!);
    }
}
