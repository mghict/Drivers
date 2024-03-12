using CaptchaConfigurations.ActionFilter;
using CaptchaConfigurations.Services;
using Driver.API.Common;
using Driver.API.Controllers;
using Driver.Common.Models;
using Driver.Domain.Entities;
using Driver.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Moneyon.Common.Data;

namespace API.Controllers;

[ApiController]
[Route("api/person")]
public class PersonController : AppBaseController
{
    private readonly PersonService personService;
    private readonly ICaptchaServices _captchaService;
    public PersonController(IHttpContextAccessor accessor, PersonService personService, ICaptchaServices captchaService)
        :base(accessor)
    {
        this.personService = personService;
        _captchaService = captchaService;
    }

    [HttpGet]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.PersonView })]
    public async Task<DataResult<PersonModel>> GetPersonsPagableAsync([FromQuery] DataRequest request)
    {
        return await personService.GetPersonPagableAsync(request);
    }

    [HttpGet]
    [Route("{personCode}")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.PersonView })]
    public async Task<UserModel> GetPersonAsync(Guid personCode)
    {
        return await personService.GetPersonAsync(personCode);
    }

    [HttpPost]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.PersonCreate })]
    public async Task CreatePersonAsync(UserCreateModel model)
    {
        await personService.CreateUserAsync(model);
    }

    [HttpPut]
    [Route("")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.PersonEdit })]
    public async Task UpdatePersonAsync(UserUpdateModel model)
    {
        await personService.UpdateUserAsync(model);
    }


    [HttpDelete]
    [Route("{personCode}")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.PersonDelete })]
    public async Task DeletePersonAsync(Guid personCode)
    {
        await personService.DeleteUserAsync(personCode);
    }

    [HttpGet]
    [Route("get-captcha")]
    public async Task<FileContentResult> GetCaptcha()
    {
        return await _captchaService.GetCaptcha();
    }

    [HttpPost]
    [Route("reset-pass")]
    [JWTAuthorization()]
    [ValidateCaptcha]
    public async Task ResetPassAsync(UserResetPass model)
    {
        CheckUser();
        await personService.UserResetPasswordAsync(model,User!);
    }

    public override bool Equals(object? obj)
    {
        return obj is PersonController controller &&
               EqualityComparer<ICaptchaServices>.Default.Equals(_captchaService, controller._captchaService);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_captchaService);
    }
}
