using AutoMapper;
using Common.Extensions;
using Domain.Entities;
using Driver.Common.Models;
using Driver.Service.IRepositories;
using Moneyon.Common.IOC;

namespace Driver.Service.Services;

[AutoRegister()]
public class RoleService
{
    private readonly IUnitOfWork _uw;
    private readonly IMapper _mp;

    public RoleService(IUnitOfWork uw, IMapper mp)
    {
        _uw = uw;
        _mp = mp;
    }

    public async Task<IEnumerable<RoleModel>> GetRolesAsync()
    {
        var roles = await _uw.RoleRepository.ReadAsync(filter: p => p.IsActive == true);

        return _mp.MapCollection<Role,RoleModel>(roles);
    }
}