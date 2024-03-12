using AutoMapper;
using Common.Extensions;
using Common.Models;
using Domain.Entities;
using Driver.Common;
using Driver.Service.IRepositories;
using Microsoft.EntityFrameworkCore;
using Moneyon.Common.ExceptionHandling;
using Moneyon.Common.IOC;

namespace Driver.Service.Services;

[AutoRegister()]
public class UserService
{
    private readonly IUnitOfWork _uw;
    private readonly IMapper _mp;

    public UserService(IUnitOfWork uw, IMapper mp)
    {
        _uw = uw;
        _mp = mp;
    }

    public async Task<UserIdentityModel> UserLoginByPass(UserLoginModel dto)
    {
        var person = await _uw.UserRepository.FirstOrDefaultAsync(filter: p => p.UserName == dto.UserName &&
                                                                   p.Password == dto.Password.Hash() &&
                                                                   p.IsActive == true,
                                                                  include:p=>p.Include(s => s.Person)
                                                                              .Include(p=>p.Role!)
                                                                              .ThenInclude(p=>p.Permissions!));

        if (person is null)
            throw new BizException(BizExceptionCode.UserNotFound);

        return _mp.Map<UserIdentityModel>(person);
    }

    public async Task<UserIdentityModel> GetUserIdentity(User? user)
    {
        var userEntity = await _uw.UserRepository.FirstOrDefaultAsync(filter: p => p.Id == user!.Id &&
                                                                                   p.IsActive == true,
                                                                      include: p => p.Include(s => s.Person)
                                                                                     .Include(p => p.Role!)
                                                                                     .ThenInclude(p => p.Permissions!));

        if (userEntity is null)
            throw new BizException(BizExceptionCode.UserNotFound);

        return _mp.Map<UserIdentityModel>(userEntity);
    }

    public async Task<UserIdentityModel> GetUserIdentityAsync(UserLoginModel dto)
    {
        var result = await _uw.UserRepository.FirstOrDefaultAsync(filter: p => p.UserName == dto.UserName && 
                                                                               p.Password == dto.Password.Hash(),
                                                                  include: p => p.Include(s => s.Person)
                                                                                     .Include(p => p.Role!)
                                                                                     .ThenInclude(p => p.Permissions!));

        if (result is null) throw new BizException(BizExceptionCode.UserNotFound);

        return _mp.Map<UserIdentityModel>(result);
    }

    public async Task<UserHeaderDto?> GetPersonByUserName(string userName)
    {
        var result = await _uw.UserRepository.FirstOrDefaultAsync(filter: p => p.UserName == userName,
                                                                  include: p=>p.Include(p=>p.Person)
                                                                               .Include(p=>p.Role)
                                                                               .ThenInclude(p=>p.Permissions!)
                                                                               .Include(p=>p.Provinces!)
                                                                               .Include(p=>p.Mines!)
                                                                  );

        return result is null ? null : new UserHeaderDto() { User = result };
    }

    public async Task<UsersToken?> GetLastTokenAsync(long userId)
    {

        var token = await _uw.UsersTokenRepository.FirstOrDefaultAsync(filter: p => p.UserId == userId && p.IsExpire == false, 
                                                                       orderBy: p => p.OrderByDescending(c => c.CreateDate));
        return token;

    }

    public async Task CreateUserToken(string userName, string token)
    {
        var user = await _uw.UserRepository.FirstOrDefaultAsync(p => p.UserName == userName);
        if (user is not null)
        {
            var userToken = new UsersToken()
            {
                Token = token,
                UserId = user.Id
            };
            await _uw.UsersTokenRepository.InsertAsync(userToken);
            await _uw.CommitAsync();
        }
    }
}
