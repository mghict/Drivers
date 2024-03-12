using Common.Models;
using Driver.Common;
using Driver.Service.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moneyon.Common.ExceptionHandling;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Driver.API.Common;

public class JwtTokenService
{

    private readonly UserService userService;
    private readonly JwtSettings jwtSettings;
    public JwtTokenService(UserService userService, IOptions<JwtSettings> _appSettings)
    {
        this.userService = userService;
        this.jwtSettings = _appSettings.Value;
    }

    public async Task<AuthenticationToken> GenerateAuthToken(UserLoginModel loginModel, JwtSettings jwtSettings)
    {
        var user = await userService.GetUserIdentityAsync(loginModel);

        if (user is null)
        {
            return null;
        }

        var secretKey = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
        DateTime expireTime = DateTime.UtcNow.AddDays(1);

        var claims = GetClaims(user, Guid.NewGuid());

        var tokenOptions = new JwtSecurityToken(
            issuer: jwtSettings.ValidIssuer,
            audience: jwtSettings.ValidAudience,
            claims: claims,
            notBefore: new DateTimeOffset(DateTime.Now).DateTime,
            expires: new DateTimeOffset(expireTime).DateTime,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256));

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return new AuthenticationToken()
        {
            Name = "Authorization",
            Value = tokenString
        };// (, (int)expirationTimeStamp.Subtract(DateTime.Now).TotalSeconds);
    }

    public async Task<UserTokensModel> GenerateTokenKey(UserLoginModel loginModel)
    {
        var UserToken = new UserTokensModel();

        var user = await userService.GetUserIdentityAsync(loginModel);

        if (user is null)
        {
            throw new BizException(BizExceptionCode.UserNotFound);
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.IssuerSigningKey)); ;
        Guid Id = Guid.Empty;
        DateTime expireTime = DateTime.UtcNow.AddDays(1);
        UserToken.Validaty = expireTime.TimeOfDay;

        var JWToken = new JwtSecurityToken(
            issuer: jwtSettings.ValidIssuer,
            audience: jwtSettings.ValidAudience,
            claims: GetClaims(user, out Id),
            notBefore: new DateTimeOffset(DateTime.Now).DateTime,
            expires: new DateTimeOffset(expireTime).DateTime,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        UserToken.Token = new JwtSecurityTokenHandler().WriteToken(JWToken);
        UserToken.UserName = user.UserName;
        UserToken.DisplayName = user.DisplayName;
        UserToken.IsValidate = user.IsActive;
        UserToken.Permissions = user.Permissions?.ToList();

        await userService.CreateUserToken(loginModel.UserName, UserToken.Token);

        return UserToken;

    }

    public async Task<UserTokensModel> GenerateTokenKey(UserIdentityModel? user)
    {
        var UserToken = new UserTokensModel();

        //var user = await userService.GetUserIdentityAsync(loginModel);

        if (user is null)
        {
            throw new BizException(BizExceptionCode.UserNotFound);
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.IssuerSigningKey)); ;
        Guid Id = Guid.Empty;
        DateTime expireTime = DateTime.UtcNow.AddDays(1);
        UserToken.Validaty = expireTime.TimeOfDay;

        var JWToken = new JwtSecurityToken(
            issuer: jwtSettings.ValidIssuer,
            audience: jwtSettings.ValidAudience,
            claims: GetClaims(user, out Id),
            notBefore: new DateTimeOffset(DateTime.Now).DateTime,
            expires: new DateTimeOffset(expireTime).DateTime,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        UserToken.Token = new JwtSecurityTokenHandler().WriteToken(JWToken);
        UserToken.UserName = user.UserName;
        UserToken.DisplayName = user.DisplayName;
        UserToken.IsValidate = user.IsActive;
        UserToken.Permissions = user.Permissions?.ToList();

        await userService.CreateUserToken(user.UserName, UserToken.Token);
        return UserToken;

    }

    private IEnumerable<Claim> GetClaims(UserIdentityModel dto, out Guid Id)
    {
        Id = Guid.NewGuid();
        return GetClaims(dto, Id);
    }
    private IEnumerable<Claim> GetClaims(UserIdentityModel dto, Guid Id)
    {
        IEnumerable<Claim> claims = new Claim[] {

        new Claim(JwtRegisteredClaimNames.UniqueName, dto.UserName),
        new Claim(JwtRegisteredClaimNames.Name, dto.DisplayName),
        new Claim("role",string.Join(",", dto.Roles)),
        new Claim("permission", string.Join(",", dto.Permissions)),
        new Claim("scope", string.Join(",", dto.Permissions)),
        new Claim("Id", Id.ToString()),
        new Claim(ClaimTypes.NameIdentifier, dto.UserName),
        new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
    };

        return claims;
    }
}
