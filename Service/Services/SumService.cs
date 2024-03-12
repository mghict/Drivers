using AutoMapper;
using Domain.Entities;
using Driver.Common.Models.Reports;
using Driver.Service.IRepositories;
using Moneyon.Common.Data;
using Moneyon.Common.IOC;

namespace Driver.Service.Services;

[AutoRegister()]
public class SumService
{
    private readonly IUnitOfWork _uw;
    private readonly IMapper _mp;

    public SumService(IUnitOfWork uw, IMapper mp)
    {
        _uw = uw;
        _mp = mp;
    }

    public async Task<DataResult<ProvinceSumModel>> ProvinceSumAsync(DataRequest request)
    {
        return await _uw.RecievedWeightRepository.GetProvinceSumAsync(request); 
    }

    public async Task<DataResult<ProvinceSumModel>> ProvinceSumAsync(DataRequest request,User user)
    {
        return await _uw.RecievedWeightRepository.GetProvinceSumAsync(request,user);
    }

    public async Task<DataResult<ProvinceMineSumModel>> ProvinceMineSumAsync(DataRequest request,int provinceId, User user)
    {
        return await _uw.RecievedWeightRepository.GetProvinceMineSumAsync(request,provinceId, user);
    }

    public async Task<DataResult<MineMaterialSumModel>> ProvinceMineMaterialSumAsync(DataRequest request, long mineId, User user)
    {
        return await _uw.RecievedWeightRepository.GetMineMaterialSumAsync(request, mineId, user);
    }

    public async Task<DataResult<CitySumModel>> CitySumAsync(DataRequest request,int provinceId)
    {
        return await _uw.RecievedWeightRepository.GetCitySumAsync(request, provinceId);
    }

    public async Task<DataResult<CitySumModel>> CitySumAsync(DataRequest request)
    {
        return await _uw.RecievedWeightRepository.GetCitySumAsync(request);
    }

    public async Task<DataResult<CitySumModel>> CitySumAsync(DataRequest request,User user)
    {
        return await _uw.RecievedWeightRepository.GetCitySumAsync(request,user);
    }

    public async Task<DataResult<MineSumModel>> MineSumAsync(DataRequest request, int cityId)
    {
        return await _uw.RecievedWeightRepository.GetMineSumAsync(request, cityId);
    }

    public async Task<DataResult<MineSumModel>> MineSumAsync(DataRequest request)
    {
        return await _uw.RecievedWeightRepository.GetMineSumAsync(request);
    }

    public async Task<DataResult<ProvinceMineSumModel>> MineSumAsync(DataRequest request,User user)
    {
        return await _uw.RecievedWeightRepository.GetMineSumAsync(request,user);
    }
}
