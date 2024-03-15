using Driver.API.Common;
using Driver.API.Controllers;
using Driver.Common.Models.Reports;
using Driver.Domain.Entities;
using Driver.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Moneyon.Common.Data;
using System.Security.Authentication.ExtendedProtection;

namespace API.Controllers;

[ApiController]
[Route("api/sum-report")]
public class SumReportController : AppBaseController
{
    private readonly SumService sumService;

    public SumReportController(IHttpContextAccessor accessor, SumService sumService)
        :base(accessor)
    {
        this.sumService = sumService;
    }

    [HttpGet]
    [Route("province")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.ProvinceReportView })]
    public async Task<DataResult<ProvinceSumModel>> ProvinceSumReportAsync([FromQuery]DataRequest request)
    {
        CheckUser();
        return await sumService.ProvinceSumAsync(request,User!);
    }

    [HttpGet]
    [Route("{provinceId}/province-mine")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.ProvinceReportView })]
    public async Task<DataResult<ProvinceMineSumModel>> ProvinceMineSumReportAsync([FromQuery] DataRequest request,int provinceId)
    {
        CheckUser();
        CheckedDataRequest(request);
        return await sumService.ProvinceMineSumAsync(request, provinceId, User!);
    }

    [HttpGet]
    [Route("{mineId}/province-mine-material")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.ProvinceReportView })]
    public async Task<DataResult<MineMaterialSumModel>> ProvinceMineMaterialSumReportAsync([FromQuery] DataRequest request, long mineId)
    {
        CheckUser();
        return await sumService.ProvinceMineMaterialSumAsync(request, mineId, User!);
    }


    [HttpGet]
    [Route("{provinceId}/city")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.ProvinceReportView })]
    public async Task<DataResult<CitySumModel>> ProvinceCitySumReportAsync([FromQuery] DataRequest request,int provinceId)
    {
        return await sumService.CitySumAsync(request, provinceId);
    }

    

    [HttpGet]
    [Route("mine")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.MineReportView })]
    public async Task<DataResult<ProvinceMineSumModel>> MineSumReportAsync([FromQuery] DataRequest request)
    {
        CheckUser();
        CheckedDataRequest(request);
        return await sumService.MineSumAsync(request,User!);
    }
}