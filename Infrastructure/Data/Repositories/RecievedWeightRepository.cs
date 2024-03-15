using Domain.Entities;
using Driver.Common.Models.Reports;
using Driver.Domain.Entities;
using Driver.Service.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Moneyon.Common.Data;
using Moneyon.Common.Data.SqlServer;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Threading;

namespace Driver.Infrastructure.Data.Repositories;

public class RecievedWeightRepository : Moneyon.Common.Data.SqlServer.SqlServerGenericRepository<RecievedWeight, long>, IRecievedWeightRepository
{
    private readonly ApplicationContext _context;
    public RecievedWeightRepository(DbContext context) : base(context)
    {
        _context =(ApplicationContext)context;
    }

    public async Task<DataResult<ProvinceSumModel>> GetProvinceSumAsync(DataRequest request,CancellationToken cancellationToken=default)
    {
        var lst =await _context.Set<Province>().Select(s => new ProvinceSumModel()
                                                            {
                                                                ProvinceId = s.Id,
                                                                ProvinceName = s.Name,
                                                            }
                                                   ).ToListAsync();
        foreach (var item in lst!)
        {
            item.MineCount = await _context.Set<Mine>().CountAsync(p => p.Location.City.ProvinceId == item.ProvinceId);
            item.AutoCount= await _context.Set<Auto>().CountAsync(p => p.Mine.Location.City.ProvinceId == item.ProvinceId);
            
            //item.TotalWeight = await _context.Set<RecievedWeight>()
            //                                 .Where(p => p.Auto.Mine.Location.City.ProvinceId == item.ProvinceId)
            //                                 .SumAsync(s=>s.Weight);
            
            //var totalDays = await _context.Set<RecievedWeight>()
            //                                 .Include(p=>p.RecievedMission!)
            //                                 .Where(p => p.Auto.Mine.Location.City.ProvinceId == item.ProvinceId &&
            //                                             p.RecievedMission!= null)
            //                                 .Select(p=> (p.RecievedMission == null ? p.SendDate : p.RecievedMission.SendDate).Subtract( p.SendDate).TotalDays)
            //                                 .ToListAsync();

            //item.TotalDays = totalDays.Sum();
            //item.AvrageDays = totalDays.Count() ==0 ? 0 : totalDays.Sum()/totalDays.Count();
        }

        return await GetDataResultAsync(request, lst.AsQueryable());
    }
    public async Task<DataResult<ProvinceSumModel>> GetProvinceSumAsync(DataRequest request,User user, CancellationToken cancellationToken = default)
    {
        var provinceIds = user.Provinces?.Select(p => p.Id)?.ToList();
        var mineIds = user.Mines?.Select(p => p.Id)?.ToList();

        var lst = user.RoleId switch
        {
            3=> await _context.Set<Province>()
                              .Where(p=> provinceIds!.Contains(p.Id))
                              .Select(s => new ProvinceSumModel()
                {
                    ProvinceId = s.Id,
                    ProvinceName = s.Name,
                }).ToListAsync(),
            4 => await _context.Set<Mine>()
                              .Where(p => mineIds!.Contains(p.Id))
                              .Include(p=>p.Location!)
                              .ThenInclude(p=>p.City!)
                              .ThenInclude(p=>p.Province!)
                              .Select(s => new ProvinceSumModel()
                              {
                                  ProvinceId = s.Location.City.ProvinceId,
                                  ProvinceName = s.Location.City.Province.Name,
                              }).ToListAsync(),
            _ => await _context.Set<Province>().Select(s => new ProvinceSumModel()
                {
                    ProvinceId = s.Id,
                    ProvinceName = s.Name,
                }).ToListAsync()
        };

        foreach (var item in lst!)
        {
            item.MineCount = await _context.Set<Mine>().CountAsync(p => p.Location.City.ProvinceId == item.ProvinceId);
            item.AutoCount = await _context.Set<Auto>().CountAsync(p => p.Mine.Location.City.ProvinceId == item.ProvinceId);
        }

        return await GetDataResultAsync(request, lst.AsQueryable());
    }

    public async Task<DataResult<ProvinceMineSumModel>> GetProvinceMineSumAsync(DataRequest request,int provinceId, User user, CancellationToken cancellationToken = default)
    {
        var provinceIds = user.Provinces?.Select(p => p.Id)?.ToList();
        var mineIds = user.Mines?.Select(p => p.Id)?.ToList();

        Expression<Func<Mine, bool>> expName=p=>p.Id>0;

        if (request is not null && request.Filters is not null)
        {
            foreach(var item in request.Filters)
            {
                if(item.Field.Trim().ToLower()=="minename" && !string.IsNullOrWhiteSpace(item.Value))
                {
                    expName = p => p.Name.Contains(item.Value!.Trim());
                }
            }
        }

        var lst = await _context.Set<Mine>()
                              .Where(p => p.Location.City.ProvinceId == provinceId)
                              .Where(expName!)
                              .Include(p => p.Location!)
                              .ThenInclude(p => p.City!)
                              .ThenInclude(p => p.Province!)
                              .Include(p => p.Materials!)
                              .Select(s => new ProvinceMineSumModel()
                              {
                                  ProvinceId = s.Location.City.ProvinceId,
                                  ProvinceName = s.Location.City.Province.Name,
                                  MineId = s.Id,
                                  MineName = s.Name,
                                  MineCode = s.MineCode,
                                  MaterialNames = s.Materials!.Select(p => p.Name).ToList()
                              }).ToListAsync();

        foreach (var item in lst!)
        {
            item.AutoCount = await _context.Set<Auto>().CountAsync(p => p.MineId == item.MineId);
        }

        return await GetDataResultAsync(request, lst.AsQueryable());
    }
    public async Task<DataResult<MineMaterialSumModel>> GetMineMaterialSumAsync(DataRequest request, long mineId, User user, CancellationToken cancellationToken = default)
    {
        Expression<Func<RecievedWeight, bool>> expDateGT = p=>p.Id>0;
        Expression<Func<RecievedWeight, bool>> expDateLT = p => p.Id > 0;

        if (request is not null && request.Filters is not null)
        {
            foreach (var item in request.Filters)
            {
                if (item.Field.Trim().ToLower() == "senddate" && 
                    !string.IsNullOrWhiteSpace(item.Value) && 
                    item.Operator==FilterOperator.GT)
                {
                    expDateGT = p => p.SendDate.Date>=Convert.ToDateTime(item.Value).Date;
                }

                if (item.Field.Trim().ToLower() == "senddate" &&
                    !string.IsNullOrWhiteSpace(item.Value) &&
                    item.Operator == FilterOperator.LT)
                {
                    expDateLT = p => p.SendDate.Date <= Convert.ToDateTime(item.Value).Date;
                }
            }
        }

        var mine = await _context.Set<Mine>()
                                 .Where(p => p.Id == mineId)
                                 .Include(p => p.Materials!)
                                 .FirstOrDefaultAsync();

        var lst = mine?.Materials?.Select(p => new MineMaterialSumModel
        {
            MaterialId=p.Id,
            MaterialName=p.Name,
            Weight=0
        }).ToList();

        foreach (var item in lst!)
        {
            item.Weight = await _context.Set<RecievedWeight>()
                                        .Where(p => p.Auto!.MineId == mineId && p.Material!.Id==item.MaterialId)
                                        .Where(expDateGT!)
                                        .Where(expDateLT!)
                                        .SumAsync(p => p.Weight);
        }

        request!.Filters = new List<DataRequestFilter>();

        return await GetDataResultAsync(request, lst.AsQueryable());
    }
    
    
    public async Task<DataResult<CitySumModel>> GetCitySumAsync(DataRequest request,int provinceId, CancellationToken cancellationToken = default)
    {
        var lst = await _context.Set<City>().Where(p=>p.ProvinceId==provinceId)
                                .Select(s => new CitySumModel()
                                            {
                                                CityId = s.Id,
                                                CityName = s.Name,
                                            }
                                        ).ToListAsync();
        foreach (var item in lst!)
        {
            item.MineCount = await _context.Set<Mine>().CountAsync(p => p.Location.CityId == item.CityId);
            item.AutoCount = await _context.Set<Auto>().CountAsync(p => p.Mine.Location.CityId == item.CityId);

            item.TotalWeight = await _context.Set<RecievedWeight>()
                                             .Where(p => p.Auto.Mine.Location.CityId == item.CityId)
                                             .SumAsync(s => s.Weight);

            var totalDays = await _context.Set<RecievedWeight>()
                                             .Include(p => p.RecievedMission!)
                                             .Where(p => p.Auto.Mine.Location.CityId == item.CityId &&
                                                         p.RecievedMission != null)
                                             .Select(p => (p.RecievedMission == null ? p.SendDate : p.RecievedMission.SendDate).Subtract(p.SendDate).TotalDays)
                                             .ToListAsync();

            item.TotalDays = totalDays.Sum();
            item.AvrageDays = totalDays.Count() == 0 ? 0 : totalDays.Sum() / totalDays.Count();
        }

        return await GetDataResultAsync(request, lst.AsQueryable());
    }
    public async Task<DataResult<CitySumModel>> GetCitySumAsync(DataRequest request,  CancellationToken cancellationToken = default)
    {
        var lst = await _context.Set<City>()
                                .Select(s => new CitySumModel()
                                {
                                    CityId = s.Id,
                                    CityName = s.Name,
                                }
                                        ).ToListAsync();
        foreach (var item in lst!)
        {
            item.MineCount = await _context.Set<Mine>().CountAsync(p => p.Location.CityId == item.CityId);
            item.AutoCount = await _context.Set<Auto>().CountAsync(p => p.Mine.Location.CityId == item.CityId);

            item.TotalWeight = await _context.Set<RecievedWeight>()
                                             .Where(p => p.Auto.Mine.Location.CityId == item.CityId)
                                             .SumAsync(s => s.Weight);

            var totalDays = await _context.Set<RecievedWeight>()
                                             .Include(p => p.RecievedMission!)
                                             .Where(p => p.Auto.Mine.Location.CityId == item.CityId &&
                                                         p.RecievedMission != null)
                                             .Select(p => (p.RecievedMission == null ? p.SendDate : p.RecievedMission.SendDate).Subtract(p.SendDate).TotalDays)
                                             .ToListAsync();

            item.TotalDays = totalDays.Sum();
            item.AvrageDays = totalDays.Count() == 0 ? 0 : totalDays.Sum() / totalDays.Count();
        }

        return await GetDataResultAsync(request, lst.AsQueryable());
    }
    public async Task<DataResult<CitySumModel>> GetCitySumAsync(DataRequest request,User user, CancellationToken cancellationToken = default)
    {
        var provinceIds = user.Provinces?.Select(p => p.Id)?.ToList();
        var mineIds = user.Mines?.Select(p => p.Id)?.ToList();

        var lst = user.RoleId switch
        {
            3 => await _context.Set<City>()
                              .Where(p => provinceIds!.Contains(p.ProvinceId))
                              .Include(p=>p.Province)
                              .Select(s => new CitySumModel()
                                          {
                                              CityId = s.Id,
                                              CityName = s.Name,
                                          }
                                    ).ToListAsync(),

            4 => await _context.Set<Mine>()
                              .Where(p => mineIds!.Contains(p.Id))
                              .Include(p => p.Location!)
                              .ThenInclude(p => p.City!)
                              .Select(s => new CitySumModel()
                                            {
                                              CityId = s.Location.CityId,
                                              CityName = s.Location.City.Name
                                            }
                                    ).ToListAsync(),

            _ => await _context.Set<City>()
                                .Select(s => new CitySumModel()
                                            {
                                                CityId = s.Id,
                                                CityName = s.Name,
                                            }
                                ).ToListAsync()
        };

        foreach (var item in lst!)
        {
            item.MineCount = await _context.Set<Mine>().CountAsync(p => p.Location.CityId == item.CityId);
            item.AutoCount = await _context.Set<Auto>().CountAsync(p => p.Mine.Location.CityId == item.CityId);

            item.TotalWeight = await _context.Set<RecievedWeight>()
                                             .Where(p => p.Auto.Mine.Location.CityId == item.CityId)
                                             .SumAsync(s => s.Weight);

            var totalDays = await _context.Set<RecievedWeight>()
                                             .Include(p => p.RecievedMission!)
                                             .Where(p => p.Auto.Mine.Location.CityId == item.CityId &&
                                                         p.RecievedMission != null)
                                             .Select(p => (p.RecievedMission == null ? p.SendDate : p.RecievedMission.SendDate).Subtract(p.SendDate).TotalDays)
                                             .ToListAsync();

            item.TotalDays = totalDays.Sum();
            item.AvrageDays = totalDays.Count() == 0 ? 0 : totalDays.Sum() / totalDays.Count();
        }

        return await GetDataResultAsync(request, lst.AsQueryable());
    }


    public async Task<DataResult<MineSumModel>> GetMineSumAsync(DataRequest request, int cityId, CancellationToken cancellationToken = default)
    {
        var lst = await _context.Set<Mine>().Where(p => p.Location.CityId == cityId)
                                .Select(s => new MineSumModel()
                                {
                                    MineId = s.Id,
                                    MineName = s.Name,
                                }
                                        ).ToListAsync();
        foreach (var item in lst!)
        {
            //item.MineCount = await _context.Set<Mine>().CountAsync(p => p.Id==item.MineId);
            item.AutoCount = await _context.Set<Auto>().CountAsync(p => p.Id == item.MineId);

            //item.TotalWeight = await _context.Set<RecievedWeight>()
            //                                 .Where(p => p.Auto.Mine.Id == item.MineId)
            //                                 .SumAsync(s => s.Weight);

            //var totalDays = await _context.Set<RecievedWeight>()
            //                                 .Include(p => p.RecievedMission!)
            //                                 .Where(p => p.Auto.Mine.Id == item.MineId &&
            //                                             p.RecievedMission != null)
            //                                 .Select(p => (p.RecievedMission == null ? p.SendDate : p.RecievedMission.SendDate).Subtract(p.SendDate).TotalDays)
            //                                 .ToListAsync();

            //item.TotalDays = totalDays.Sum();
            //item.AvrageDays = totalDays.Count() == 0 ? 0 : totalDays.Sum() / totalDays.Count();
        }

        return await GetDataResultAsync(request, lst.AsQueryable());
    }
    public async Task<DataResult<MineSumModel>> GetMineSumAsync(DataRequest request, CancellationToken cancellationToken = default)
    {
        var lst = await _context.Set<Mine>()
                                .Select(s => new MineSumModel()
                                {
                                    MineId = s.Id,
                                    MineName = s.Name,
                                }
                                        ).ToListAsync();
        foreach (var item in lst!)
        {    
            item.AutoCount = await _context.Set<Auto>().CountAsync(p => p.Id == item.MineId);
        }

        return await GetDataResultAsync(request, lst.AsQueryable());
    }
    public async Task<DataResult<ProvinceMineSumModel>> GetMineSumAsync(DataRequest request,User user, CancellationToken cancellationToken = default)
    {
        Expression<Func<Mine, bool>> expName = p=>p.Id>0;
        Expression<Func<Mine, bool>> expProvinceName = p=>p.Id>0;
        if (request is not null && request.Filters is not null)
        {
            foreach (var item in request.Filters)
            {
                if (item.Field.Trim().ToLower() == "minename" && !string.IsNullOrWhiteSpace(item.Value))
                {
                    expName = p => p.Name.Contains(item.Value!.Trim());
                }

                if (item.Field.Trim().ToLower() == "provincename" && !string.IsNullOrWhiteSpace(item.Value))
                {
                    expProvinceName = p => p.Location.City.Province.Name.Contains(item.Value!.Trim());
                }
            }
        }

        

        var provinceIds = user.Provinces?.Select(p => p.Id)?.ToList();
        var mineIds = user.Mines?.Select(p => p.Id)?.ToList();

        var lst = user.RoleId switch
        {
            3 => await _context.Set<Mine>()
                                .Where(p=>provinceIds!.Contains(p.Location.City.ProvinceId))
                                .Where(expName!)
                                .Where(expProvinceName!)
                                .Include(p => p.Location!)
                                .ThenInclude(p => p.City!)
                                .ThenInclude(p => p.Province!)
                                .Include(p => p.Materials!)
                                .OrderBy(p=>p.Location.City.ProvinceId)
                                .ThenBy(p=>p.MineCode)
                                .Select(s => new ProvinceMineSumModel()
                                            {
                                                MineId = s.Id,
                                                MineName = s.Name,
                                                MineCode = s.MineCode,
                                                ProvinceId = s.Location.City.ProvinceId,
                                                ProvinceName = s.Location.City.Province.Name,
                                                MaterialNames = s.Materials!.Select(p => p.Name).ToList()
                                            }
                                        ).ToListAsync(),
            4 => await _context.Set<Mine>()
                                .Where(p => mineIds!.Contains(p.Id))
                                .Where(expName!)
                                .Where(expProvinceName!)
                                .Include(p => p.Location!)
                                .ThenInclude(p => p.City!)
                                .ThenInclude(p => p.Province!)
                                .Include(p => p.Materials!)
                                .OrderBy(p => p.Location.City.ProvinceId)
                                .ThenBy(p => p.MineCode)
                                .Select(s => new ProvinceMineSumModel()
                                            {
                                                MineId = s.Id,
                                                MineName = s.Name,
                                                MineCode = s.MineCode,
                                                ProvinceId = s.Location.City.ProvinceId,
                                                ProvinceName = s.Location.City.Province.Name,
                                                MaterialNames = s.Materials!.Select(p => p.Name).ToList()
                                            }
                                        ).ToListAsync(),
            _ => await _context.Set<Mine>()
                               .Where(expName!)
                               .Where(expProvinceName!)
                               .Include(p => p.Location!)
                               .ThenInclude(p => p.City!)
                               .ThenInclude(p => p.Province!)
                               .Include(p => p.Materials!)
                               .OrderBy(p => p.Location.City.ProvinceId)
                               .ThenBy(p => p.MineCode)
                               .Select(s => new ProvinceMineSumModel()
                                            {
                                                MineId = s.Id,
                                                MineName = s.Name,
                                                MineCode=s.MineCode,
                                                ProvinceId=s.Location.City.ProvinceId,
                                                ProvinceName=s.Location.City.Province.Name,
                                                MaterialNames=s.Materials!.Select(p=>p.Name).ToList()
                                            }
                                        ).ToListAsync()
        };

        foreach (var item in lst!)
        {
            item.AutoCount = await _context.Set<Auto>()
                                           .Where(p => p.MineId == item.MineId)
                                           .CountAsync();
        }
        
        request!.Filters = new List<DataRequestFilter>();

        return await GetDataResultAsync(request, lst.AsQueryable());
    }


    private async Task<DataResult<ProvinceSumModel>> GetDataResultAsync(DataRequest? request, IQueryable<ProvinceSumModel> query, CancellationToken cancellationToken = default)
    {
        if (request != null)
        {
            if (request!.Filters != null && request!.Filters.Count() > 0)
            {
                query = query.Where(EntityExtension.GetWherePredicate<ProvinceSumModel>(request!.Filters));
            }

            if (request!.Sort != null && !string.IsNullOrWhiteSpace(request!.Sort!.Field))
            {
                Func<IQueryable<ProvinceSumModel>, IOrderedQueryable<ProvinceSumModel>> orderBy = EntityExtension.GetOrderBy<ProvinceSumModel>(request!.Sort);
                query = orderBy(query);
            }
        }

        long total = query.LongCount();
        List<ProvinceSumModel> data = query.ApplyDataRequest(request ?? new DataRequest()).ToList();
        return new DataResult<ProvinceSumModel>(request ?? new DataRequest(), data, total);
    }
    private async Task<DataResult<CitySumModel>> GetDataResultAsync(DataRequest? request, IQueryable<CitySumModel> query, CancellationToken cancellationToken = default)
    {
        if (request != null)
        {
            if (request!.Filters != null && request!.Filters.Count() > 0)
            {
                query = query.Where(EntityExtension.GetWherePredicate<CitySumModel>(request!.Filters));
            }

            if (request!.Sort != null && !string.IsNullOrWhiteSpace(request!.Sort!.Field))
            {
                Func<IQueryable<CitySumModel>, IOrderedQueryable<CitySumModel>> orderBy = EntityExtension.GetOrderBy<CitySumModel>(request!.Sort);
                query = orderBy(query);
            }
        }

        long total = query.LongCount();
        List<CitySumModel> data = query.ApplyDataRequest(request ?? new DataRequest()).ToList();
        return new DataResult<CitySumModel>(request ?? new DataRequest(), data, total);
    }
    private async Task<DataResult<MineSumModel>> GetDataResultAsync(DataRequest? request, IQueryable<MineSumModel> query, CancellationToken cancellationToken = default)
    {
        if (request != null)
        {
            //if (request!.Filters != null && request!.Filters.Count() > 0)
            //{
            //    query = query.Where(EntityExtension.GetWherePredicate<MineSumModel>(request!.Filters));
            //}

            if (request!.Sort != null && !string.IsNullOrWhiteSpace(request!.Sort!.Field))
            {
                Func<IQueryable<MineSumModel>, IOrderedQueryable<MineSumModel>> orderBy = EntityExtension.GetOrderBy<MineSumModel>(request!.Sort);
                query = orderBy(query);
            }
        }

        long total = query.LongCount();
        List<MineSumModel> data = query.ApplyDataRequest(request ?? new DataRequest()).ToList();
        return new DataResult<MineSumModel>(request ?? new DataRequest(), data, total);
    }
    private async Task<DataResult<ProvinceMineSumModel>> GetDataResultAsync(DataRequest? request, IQueryable<ProvinceMineSumModel> query, CancellationToken cancellationToken = default)
    {
        if (request != null)
        {

            if (request!.Sort != null && !string.IsNullOrWhiteSpace(request!.Sort!.Field))
            {
                Func<IQueryable<ProvinceMineSumModel>, IOrderedQueryable<ProvinceMineSumModel>> orderBy = EntityExtension.GetOrderBy<ProvinceMineSumModel>(request!.Sort);
                query = orderBy(query);
            }
        }

        long total = query.LongCount();
        List<ProvinceMineSumModel> data = query.ApplyDataRequest(request ?? new DataRequest()).ToList();
        return new DataResult<ProvinceMineSumModel>(request ?? new DataRequest(), data, total);
    }
    private async Task<DataResult<MineMaterialSumModel>> GetDataResultAsync(DataRequest? request, IQueryable<MineMaterialSumModel> query, CancellationToken cancellationToken = default)
    {
        if (request != null)
        {
            if (request!.Sort != null && !string.IsNullOrWhiteSpace(request!.Sort!.Field))
            {
                Func<IQueryable<MineMaterialSumModel>, IOrderedQueryable<MineMaterialSumModel>> orderBy = EntityExtension.GetOrderBy<MineMaterialSumModel>(request!.Sort);
                query = orderBy(query);
            }
        }

        long total = query.LongCount();
        List<MineMaterialSumModel> data = query.ApplyDataRequest(request ?? new DataRequest()).ToList();
        return new DataResult<MineMaterialSumModel>(request ?? new DataRequest(), data, total);
    }
}
