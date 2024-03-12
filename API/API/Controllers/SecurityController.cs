using CaptchaConfigurations.ActionFilter;
using CaptchaConfigurations.Services;
using Common.Extensions;
using Common.Models;
using Driver.API.Common;
using Driver.API.Controllers;
using Driver.Common;
using Driver.Domain.Entities;
using Driver.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Moneyon.Common.ExceptionHandling;

namespace API.Controllers;

[ApiController]
[Route("api/security")]
public class SecurityController : AppBaseController
{
    private readonly ICaptchaServices _captchaService;
    private readonly UserService _userService;
    private readonly JwtTokenService _jwtTokenService;
    public SecurityController(IHttpContextAccessor contextAccessor, ICaptchaServices captchaService, UserService userService, JwtTokenService jwtTokenService)
        : base(contextAccessor)
    {
        _captchaService = captchaService;
        _userService = userService;
        _jwtTokenService = jwtTokenService;
    }

    [HttpGet]
    [Route("get-captcha")]
    public async Task<FileContentResult> GetCaptcha()
    {
        return await _captchaService.GetCaptcha();
    }


    [HttpPost]
    [Route("login")]
    [ValidateCaptcha]
    public async Task<UserTokensModel> LoginUser(UserLoginModel model)
    {
        var user = await _userService.UserLoginByPass(model);
        return await _jwtTokenService.GenerateTokenKey(user);
    }

    [HttpGet]
    [Route("identity")]
    //[JWTAuthorization()]
    public async Task<UserIdentityModel> GetIdentity()
    {
        CheckUser();

        return await _userService.GetUserIdentity(User);
    }

    [HttpGet]
    [Route("hashPass/{pass}")]
    public async Task<string> HashPassUser(string pass)
    {
        return pass.Hash();
    }

    }
