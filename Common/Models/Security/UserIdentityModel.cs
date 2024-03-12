namespace Common.Models;

public class UserIdentityModel
{
    public string PersonCode { get; set; }
    public string UserName { get; set; }
    public string DisplayName { get; set; }
    public string?[] Roles { get; set; }
    public string?[] Permissions { get; set; }

    public bool IsActive { get; set; }
    public UserIdentityModel()
    {

    }
    public UserIdentityModel(string userName, string displayName, bool isActive, string?[] roles, string?[] permissions)
    {
        UserName = userName;
        DisplayName = displayName;
        Roles = roles;
        Permissions = permissions;
        IsActive = isActive;
    }
}
