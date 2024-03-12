using Domain.Entities;
using Driver.Common;
using Microsoft.AspNetCore.Mvc;
using Moneyon.Common.Data;
using Moneyon.Common.ExceptionHandling;

namespace Driver.API.Controllers;

[ApiController]
public class AppBaseController : ControllerBase
{
    protected readonly User? User;
    protected HttpContext Context { get; set; }
    public AppBaseController(IHttpContextAccessor contextAccessor) : base()
    {
        Context = contextAccessor!.HttpContext;
        var contextUser = Context!.Items["User"];
        if (contextUser is not null)
            User = contextUser as User;
    }


    protected void CheckUser()
    {
        if (User is null)
            throw new BizException(BizExceptionCode.UserNotFound);
    }

    protected void CheckedDataRequest(DataRequest request)
    {
        if (request is null)
            return;
        if (request.Filters is null || request.Filters.Count() == 0)
            return;
        foreach (var item in request.Filters)
            if(!string.IsNullOrWhiteSpace( item.Field))
                item.Field = $"{item.Field.Substring(0, 1).ToUpper()}{item.Field.Substring(1)}";

        

    }
}
