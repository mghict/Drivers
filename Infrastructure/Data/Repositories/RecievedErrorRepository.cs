using Domain.Entities;
using Driver.Common.Models.Reports;
using Driver.Domain.Entities;
using Driver.Service.IRepositories;
using Microsoft.EntityFrameworkCore;
using Moneyon.Common.Data;
using Moneyon.Common.Data.SqlServer;
using System.Linq.Expressions;

namespace Driver.Infrastructure.Data.Repositories;

public class RecievedErrorRepository : Moneyon.Common.Data.SqlServer.SqlServerGenericRepository<RecievedError, long>, IRecievedErrorRepository
{
    public RecievedErrorRepository(DbContext context) : base(context)
    {
    }

    public async Task<DataResult<RecievedError>> GetAutoErrorsPagableAsync(DataRequest request,long autoId)
    {
        Expression<Func<RecievedError, bool>> expErrorMessage = p => p.Id > 0;
        Expression<Func<RecievedError, bool>> expDateGT = p => p.Id > 0;
        Expression<Func<RecievedError, bool>> expDateLT = p => p.Id > 0;

        if (request is not null && request.Filters is not null)
        {
            foreach (var item in request.Filters)
            {
                if (item.Field.Trim().ToLower() == "errorcode" &&
                    !string.IsNullOrWhiteSpace(item.Value))
                {
                    expErrorMessage = p => p.ErrorCode.ErrorMessage.Contains(item.Value);
                }

                if (item.Field.Trim().ToLower() == "senddate" &&
                    !string.IsNullOrWhiteSpace(item.Value) &&
                    item.Operator == FilterOperator.GT)
                {
                    expDateGT = p => p.SendDate.Date >= Convert.ToDateTime(item.Value).Date;
                }

                if (item.Field.Trim().ToLower() == "senddate" &&
                    !string.IsNullOrWhiteSpace(item.Value) &&
                    item.Operator == FilterOperator.LT)
                {
                    expDateLT = p => p.SendDate.Date <= Convert.ToDateTime(item.Value).Date;
                }
            }
        }

        var lst =  await dbSet.AsQueryable()
                              .Where(p => p.AutoId== autoId)
                              .Where(expErrorMessage)
                              .Where(expDateGT)
                              .Where(expDateLT)
                              .Include(p => p.ErrorCode)
                              .OrderByDescending(p=>p.SendDate).ThenByDescending(p=>p.RowNumber)
                              .ToListAsync();

        request!.Filters = new List<DataRequestFilter>();

        return await GetDataResultAsync(request, lst.AsQueryable());
    }

    private async Task<DataResult<RecievedError>> GetDataResultAsync(DataRequest? request, IQueryable<RecievedError> query, CancellationToken cancellationToken = default)
    {
        if (request != null)
        {
            if (request!.Sort != null && !string.IsNullOrWhiteSpace(request!.Sort!.Field))
            {
                Func<IQueryable<RecievedError>, IOrderedQueryable<RecievedError>> orderBy = EntityExtension.GetOrderBy<RecievedError>(request!.Sort);
                query = orderBy(query);
            }
        }

        long total = query.LongCount();
        List<RecievedError> data = query.ApplyDataRequest(request ?? new DataRequest()).ToList();
        return new DataResult<RecievedError>(request ?? new DataRequest(), data, total);
    }
}
