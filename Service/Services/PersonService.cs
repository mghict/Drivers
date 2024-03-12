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
public class PersonService
{
    private readonly IUnitOfWork _uw;
    private readonly IMapper _mp;

    public PersonService(IUnitOfWork uw, IMapper mp)
    {
        _uw = uw;
        _mp = mp;
    }

    public async Task<DataResult<PersonModel>> GetPersonPagableAsync(DataRequest request)
    {
        var persons = await _uw.PersonRepository.ReadPagableAsync(request, 
                                                                  filter: p => p.Type == PersonType.Users,
                                                                  include: p => p.Include(u => u.User!));
        return _mp.MapDataResult<Person,PersonModel>(persons);
    }

    public async Task<UserModel> GetPersonAsync(Guid personCode)
    {
        var person= await _uw.PersonRepository.FirstOrDefaultAsync(filter: p => p.Type == PersonType.Users && p.PersonCode==personCode,
                                                                   include: p => p.Include(u => u.User!)
                                                                                .ThenInclude(p => p.Role!)
                                                                                .Include(p => p.User!)
                                                                                .ThenInclude(p => p.Provinces!)
                                                                                .Include(p => p.User!)
                                                                                .ThenInclude(p => p.Mines!)
                                                                  );
        if (person is null)
            throw new BizException(BizExceptionCode.DataNotFound);

        return _mp.Map<UserModel>(person);
    }

    public async Task CreateUserAsync(UserCreateModel model)
    {
        var exists = await _uw.PersonRepository.AnyAsync(filter: p => p.NationalCode.Trim() == model.NationalCode.Trim() &&
                                                                      p.Type==PersonType.Users);
        if (exists)
            throw new BizException(BizExceptionCode.DataIsExists);

        var mineIds = model.Mines?.Select(p => p.Id).ToList();
        var mines = await _uw.MineRepository.ReadAsync(filter: p => mineIds.Contains(p.Id));

        var provinceIds = model.Provinces?.Select(p => p.Id).ToList();
        var provinces = await _uw.ProvinceRepository.ReadAsync(filter: p => provinceIds.Contains(p.Id));

        Person entity = _mp.Map<Person>(model);
        entity.User = new User()
        {
            
            IsActive = true,
            RoleId = model.Role.Id,
            Password = model.NationalCode.Hash(),
            UserName = model.MobileNumber,
            Mines = mines.ToList(),
            Provinces = provinces.ToList(),
            LastChanged = DateTime.UtcNow
        };

        try
        {
            await _uw.PersonRepository.InsertAsync(entity);
            await _uw.CommitAsync();
        }
        catch(Exception ex)
        {
            throw ex;
        }

    }

    public async Task UpdateUserAsync(UserUpdateModel model)
    {
        var exists = await _uw.PersonRepository.AnyAsync(filter: p => p.NationalCode.Trim() == model.NationalCode.Trim() &&
                                                                      p.Type==PersonType.Users &&
                                                                      p.PersonCode!=model.PersonCode );
        if (exists)
            throw new BizException(BizExceptionCode.DataIsExists);

        var mineIds = model.Mines?.Select(p => p.Id).ToList();
        var mines = await _uw.MineRepository.ReadAsync(filter: p => mineIds.Contains(p.Id));

        var provinceIds = model.Provinces?.Select(p => p.Id).ToList();
        var provinces = await _uw.ProvinceRepository.ReadAsync(filter: p => provinceIds.Contains(p.Id));

        var entity = await _uw.PersonRepository.FirstOrDefaultAsync(filter: p => p.Type == PersonType.Users &&
                                                                                 p.PersonCode == model.PersonCode,
                                                                    include:p=>p.Include(u=>u.User!)
                                                                                .ThenInclude(p=>p.Role!)
                                                                                .Include(p=>p.User!)
                                                                                .ThenInclude(p=>p.Provinces!)
                                                                                .Include(p => p.User!)
                                                                                .ThenInclude(p => p.Mines!));

        _mp.Map(model,entity);

        foreach (var item in entity!.User.Mines!)
        {
            entity!.User!.Mines!.Remove(item);
        }

        foreach (var item in entity!.User.Provinces!)
        {
            entity!.User!.Provinces!.Remove(item);
        }
        entity!.User!.RoleId = model.Role.Id;
        entity!.User!.IsActive = model.IsActive;
        entity!.User!.Mines = mines.ToList();
        entity!.User!.Provinces = provinces.ToList();

        await _uw.CommitAsync();

    }

    public async Task UserResetPasswordAsync(UserResetPass model,User user)
    {
        var entity = await _uw.PersonRepository.FirstOrDefaultAsync(filter: p => p.Id == user.Id && p.Type == PersonType.Users,
                                                                    include: p=>p.Include(p=>p.User!));
        if (entity is null || entity.User is null)
            throw new BizException(BizExceptionCode.UserNotFound);

        if(entity.User.Password!=model.CurrentPassword.Hash())
            throw new BizException(BizExceptionCode.PasswordInvalid);

        entity.User.Password=model.NewPassword.Hash();

        await _uw.CommitAsync();

    }

    public async Task DeleteUserAsync(Guid personCode)
    {

        var entity = await _uw.PersonRepository.FirstOrDefaultAsync(filter: p => p.Type == PersonType.Users &&
                                                                                 p.PersonCode == personCode);

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