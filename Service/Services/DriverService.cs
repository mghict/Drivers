using AutoMapper;
using Common.Extensions;
using Domain.Entities;
using Driver.Common;
using Driver.Common.Models;
using Driver.Service.IRepositories;
using Microsoft.EntityFrameworkCore;
using Moneyon.Common.Data;
using Moneyon.Common.ExceptionHandling;
using Moneyon.Common.IOC;

namespace Driver.Service.Services;

[AutoRegister()]
public class DriverService
{
    private readonly IUnitOfWork _uw;
    private readonly IMapper _mp;

    public DriverService(IUnitOfWork uw, IMapper mp)
    {
        _uw = uw;
        _mp = mp;
    }

    public async Task<DataResult<PersonModel>> GetDriversPagableAsync(DataRequest request)
    {
        var drivers= await _uw.PersonRepository.ReadPagableAsync(request,filter: p=>p.Type==PersonType.Drivers);
        return _mp.MapDataResult<Person, PersonModel>(drivers);
    }

    public async Task<IEnumerable<PersonShortModel>> GetDriversAsync()
    {
        var drivers = await _uw.PersonRepository.ReadAsync(filter: p => p.Type == PersonType.Drivers &&
                                                                        p.IsActive==true);
        return _mp.MapCollection<Person, PersonShortModel>(drivers);
    }

    public async Task<PersonModel> GetDriverAsync(Guid personCode)
    {
        var driver = await _uw.PersonRepository.FirstOrDefaultAsync(filter: p => p.Type == PersonType.Drivers && p.PersonCode == personCode,
                                                                    include:p=>p.Include(p=>p.Document!));
        if (driver is null)
            throw new BizException(BizExceptionCode.DataNotFound);

        return _mp.Map<PersonModel>(driver);
    }

    public async Task CreateDriverAsync(PersonCreateModel model)
    {
        var exists = await _uw.PersonRepository.AnyAsync(p => p.NationalCode == model.NationalCode && p.Type == PersonType.Drivers);
        if (exists)
            throw new BizException(BizExceptionCode.DataIsExists);

        var entity = _mp.Map<Person>(model);
        entity.Type = PersonType.Drivers;

        await _uw.PersonRepository.InsertAsync(entity);
        await _uw.CommitAsync();
    }

    public async Task UpdateDriverAsync(PersonUpdateModel model)
    {
        var exists = await _uw.PersonRepository.AnyAsync(p => p.NationalCode == model.NationalCode && 
                                                              p.Type == PersonType.Drivers && 
                                                              p.PersonCode!=model.PersonCode);
        if (exists)
            throw new BizException(BizExceptionCode.DataIsExists);

        var entity = await _uw.PersonRepository.FirstOrDefaultAsync(p => p.PersonCode == model.PersonCode && p.Type == PersonType.Drivers);
        if (entity is null)
            throw new BizException(BizExceptionCode.DataNotFound);
            
       _mp.Map(model,entity);

        await _uw.CommitAsync();
    }

    public async Task DeleteDriverAsync(Guid personCode)
    {
        var entity = await _uw.PersonRepository.FirstOrDefaultAsync(p => p.PersonCode == personCode && p.Type == PersonType.Drivers);
        if (entity is null)
            throw new BizException(BizExceptionCode.DataNotFound);

        await _uw.PersonRepository.DeleteAsync(entity);

        try
        {
            await _uw.CommitAsync();
        }
        catch (Exception ex)
        {
            throw new BizException(BizExceptionCode.General_DeleteNotComplete);
        }
    }
}
