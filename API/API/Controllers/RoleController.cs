using Driver.API.Common;
using Driver.Common.Models;
using Driver.Domain.Entities;
using Driver.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/role")]
public class RoleController :ControllerBase
{
    private readonly RoleService roleService;

    public RoleController(RoleService roleService)
    {
        this.roleService = roleService;
    }

    [HttpGet]
    [Route("roles")]
    [JWTAuthorization()]
    public async Task<IEnumerable<RoleModel>> GetRolesAsync()
    {
        return await roleService.GetRolesAsync();
    }
}