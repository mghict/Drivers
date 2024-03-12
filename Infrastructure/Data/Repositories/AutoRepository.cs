using DocumentFormat.OpenXml.InkML;
using Domain.Entities;
using Driver.Common.Models;
using Driver.Common.Models.Reports;
using Driver.Service.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Moneyon.Common.Data;

namespace Driver.Infrastructure.Data.Repositories;

public class AutoRepository : Moneyon.Common.Data.SqlServer.SqlServerGenericRepository<Auto, long>, IAutoRepository
{
    private readonly ApplicationContext _context;
    public AutoRepository(DbContext context) : base(context)
    {
        _context = (ApplicationContext)context;
    }

    public async Task<DataResult<Auto>> GetAutosPagableAsync(DataRequest request, User user)
    {
        var provinceIds = user.Provinces?.Select(p => p.Id)?.ToList();
        var mineIds = user.Mines?.Select(p => p.Id)?.ToList();

        var lst = user.RoleId switch
        {
            3 => await ReadPagableAsync(request,filter: p => provinceIds!.Contains(p.Mine.Location.City.ProvinceId),
                                        include: p => p.Include(p => p.Mine)
                                                        .ThenInclude(p => p.Location)
                                                        .Include(p => p.Person)
                                                        .Include(p => p.AutoModel)
                                                        .ThenInclude(p => p.AutoBrand)),
            4 => await ReadPagableAsync(request, filter: p => mineIds!.Contains(p.MineId),
                                        include: p => p.Include(p => p.Mine)
                                                        .ThenInclude(p => p.Location)
                                                        .Include(p => p.Person)
                                                        .Include(p => p.AutoModel)
                                                        .ThenInclude(p => p.AutoBrand)),
            _ => await ReadPagableAsync(request,
                                        include: p => p.Include(p => p.Mine)
                                                        .ThenInclude(p => p.Location)
                                                        .Include(p => p.Person)
                                                        .Include(p => p.AutoModel)
                                                        .ThenInclude(p => p.AutoBrand))
        };

        return lst;

    }
}
