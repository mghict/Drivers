using DocumentFormat.OpenXml.InkML;
using Domain.Entities;
using Driver.Common.Models;
using Driver.Common.Models.Reports;
using Driver.Domain.Entities;
using Driver.Service.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Moneyon.Common.Data;
using Moneyon.Common.Data.SqlServer;
using System.Linq.Expressions;

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

        Expression<Func<Auto, bool>> expDeviceCode = p => p.Id > 0;

        if (request is not null && request.Filters is not null)
        {
            foreach (var item in request.Filters)
            {
                if (item.Field.Trim().ToLower() == "devicecode" &&
                    !string.IsNullOrWhiteSpace(item.Value))
                {
                    expDeviceCode = p => p.DeviceCode.ToString().Contains(item.Value);
                }
            }
        }

        var lst = user.RoleId switch
        {
            3 => await dbSet.AsQueryable().Where(p => provinceIds!.Contains(p.Mine.Location.City.ProvinceId))
                                          .Where(expDeviceCode)
                                          .Include(p => p.Mine)
                                          .ThenInclude(p => p.Location)
                                          .Include(p => p.Person)
                                          .Include(p => p.AutoModel)
                                          .ThenInclude(p => p.AutoBrand)
                                          .ToListAsync(),

            4 => await dbSet.AsQueryable().Where(p => mineIds!.Contains(p.MineId))
                                          .Where(expDeviceCode)
                                          .Include(p => p.Mine)
                                          .ThenInclude(p => p.Location)
                                          .Include(p => p.Person)
                                          .Include(p => p.AutoModel)
                                          .ThenInclude(p => p.AutoBrand)
                                          .ToListAsync(),

            _ => await dbSet.AsQueryable().Where(expDeviceCode)
                                          .Include(p => p.Mine)
                                          .ThenInclude(p => p.Location)
                                          .Include(p => p.Person)
                                          .Include(p => p.AutoModel)
                                          .ThenInclude(p => p.AutoBrand)
                                          .ToListAsync()
        };

        request!.Filters = new List<DataRequestFilter>();

        return await GetDataResultAsync(request, lst.AsQueryable());

    }

    private async Task<DataResult<Auto>> GetDataResultAsync(DataRequest? request, IQueryable<Auto> query, CancellationToken cancellationToken = default)
    {
        if (request != null)
        {
            if (request!.Sort != null && !string.IsNullOrWhiteSpace(request!.Sort!.Field))
            {
                Func<IQueryable<Auto>, IOrderedQueryable<Auto>> orderBy = EntityExtension.GetOrderBy<Auto>(request!.Sort);
                query = orderBy(query);
            }
        }

        long total = query.LongCount();
        List<Auto> data = query.ApplyDataRequest(request ?? new DataRequest()).ToList();
        return new DataResult<Auto>(request ?? new DataRequest(), data, total);
    }
}
