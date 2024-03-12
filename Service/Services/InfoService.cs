using AutoMapper;
using Common.Extensions;
using Domain.Entities;
using Domain.Interface;
using Driver.Common;
using Driver.Common.Models;
using Driver.Service.IRepositories;
using Microsoft.EntityFrameworkCore;
using Moneyon.Common.Data;
using Moneyon.Common.ExceptionHandling;
using Moneyon.Common.IOC;

namespace Driver.Service.Services;

[AutoRegister()]
public class InfoService
{
    private readonly IUnitOfWork _uw;
    private readonly IMapper _mp;

    public InfoService(IUnitOfWork uw, IMapper mp)
    {
        _uw = uw;
        _mp = mp;
    }

    public async Task<IEnumerable<ProvinceShortModel>> GetProvincesAsync()
    {
        var provinces= await _uw.ProvinceRepository.ReadAsync();
        return _mp.MapCollection<Province, ProvinceShortModel>(provinces);
    }

    public async Task<IEnumerable<CityShortModel>> GetProvinceCitiesAsync(int provinceId)
    {
        var cities = await _uw.CityRepository.ReadAsync(filter: p=>p.ProvinceId==provinceId);
        return _mp.MapCollection<City, CityShortModel>(cities);
    }

    #region Province

    public async Task CreateProvinceAsync(ProvinceShortModel model)
    {
        var exists = await _uw.ProvinceRepository.AnyAsync(p => p.Name.Trim().ToLower() == model.Name.ToLower().Trim());
        if (exists)
            throw new BizException(BizExceptionCode.DataIsExists);

        var province = new Province
        {
            CountryId = 1,
            Name = model.Name
        };

        await _uw.ProvinceRepository.InsertAsync(province);
        await _uw.CommitAsync();
    }
    public async Task UpdateProvinceAsync(ProvinceShortModel model)
    {
        var exists = await _uw.ProvinceRepository.AnyAsync(p => p.Name.Trim().ToLower() == model.Name.ToLower().Trim() &&
                                                                p.Id!=model.Id);
        if (exists)
            throw new BizException(BizExceptionCode.DataIsExists);

        var province =await _uw.ProvinceRepository.FirstOrDefaultAsync(p => p.Id == model.Id);
        if(province is null)
            throw new BizException(BizExceptionCode.DataNotFound);

        _mp.Map(model, province);

        await _uw.CommitAsync();
    }
    public async Task DeleteProvinceAsync(int id)
    {
        var province = await _uw.ProvinceRepository.FirstOrDefaultAsync(p => p.Id == id);
        if (province is null)
            throw new BizException(BizExceptionCode.DataNotFound);

        await _uw.ProvinceRepository.DeleteAsync(province);

        try
        {
            await _uw.CommitAsync();
        }
        catch (Exception ex)
        {
            throw new BizException(BizExceptionCode.General_DeleteNotComplete);
        };
    }

    public async Task<DataResult<ProvinceShortModel>> GetProvincePagableAsync(DataRequest request)
    {
        var provinces = await _uw.ProvinceRepository.ReadPagableAsync(request);
        return _mp.MapDataResult<Province, ProvinceShortModel>(provinces);
    }
    public async Task<ProvinceShortModel> GetProvinceAsync(int id)
    {
        var province = await _uw.ProvinceRepository.FirstOrDefaultAsync(p=>p.Id==id);
        if (province is null)
            throw new BizException(BizExceptionCode.DataNotFound);

        return _mp.Map<ProvinceShortModel>(province);
    }

    #endregion

    #region City

    public async Task CreateCityAsync(CityShortModel model)
    {
        var exists = await _uw.CityRepository.AnyAsync(p => p.Name.Trim().ToLower() == model.Name.ToLower().Trim() &&
                                                            p.ProvinceId==model.ProvinceId);
        if (exists)
            throw new BizException(BizExceptionCode.DataIsExists);

        var city = new City
        {
            ProvinceId = model.ProvinceId,
            Name = model.Name
        };

        await _uw.CityRepository.InsertAsync(city);
        await _uw.CommitAsync();
    }
    public async Task UpdateCityAsync(CityShortModel model)
    {
        var exists = await _uw.CityRepository.AnyAsync(p => p.Name.Trim().ToLower() == model.Name.ToLower().Trim() &&
                                                            p.ProvinceId==model.ProvinceId &&
                                                            p.Id != model.Id);
        if (exists)
            throw new BizException(BizExceptionCode.DataIsExists);

        var city = await _uw.CityRepository.FirstOrDefaultAsync(p => p.Id == model.Id);
        if (city is null)
            throw new BizException(BizExceptionCode.DataNotFound);

        _mp.Map(model, city);

        await _uw.CommitAsync();
    }

    public async Task DeleteCityAsync(int id)
    {
        var city = await _uw.CityRepository.FirstOrDefaultAsync(p => p.Id == id);
        if (city is null)
            throw new BizException(BizExceptionCode.DataNotFound);

        await _uw.CityRepository.DeleteAsync(city);

        try
        {
            await _uw.CommitAsync();
        }
        catch (Exception ex)
        {
            throw new BizException(BizExceptionCode.General_DeleteNotComplete);
        }
    }
    public async Task<DataResult<CityModel>> GetCityPagableAsync(DataRequest request)
    {
        var cities = await _uw.CityRepository.ReadPagableAsync(request,include:p=>p.Include(p=>p.Province));
        return _mp.MapDataResult<City, CityModel>(cities);
    }
    public async Task<CityShortModel> GetCityAsync(int id)
    {
        var city = await _uw.CityRepository.FirstOrDefaultAsync(p => p.Id == id, include: p => p.Include(p => p.Province));
        if (city is null)
            throw new BizException(BizExceptionCode.DataNotFound);

        return _mp.Map<CityShortModel>(city);
    }

    #endregion
}