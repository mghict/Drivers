namespace Common.Models;

public class UserTokensModel
{
    public string Token { get; set; }
    public string UserName { get; set; }
    public string DisplayName { get; set; }
    public bool IsValidate { get; set; } = false;
    public TimeSpan Validaty { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpiredTime { get; set; }
    public ICollection<string>? Permissions { get; set; }
    //public ICollection<string>? Roles { get; set; }
}
