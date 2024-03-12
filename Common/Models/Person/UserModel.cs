using System.ComponentModel.DataAnnotations;

namespace Driver.Common.Models;

public class UserModel
{
    public Guid PersonCode { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalCode { get; set; }
    public string? Email { get; set; }
    public string MobileNumber { get; set; }
    public string DisplayName { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
    public bool IsActive { get; set; }

    public string UserName { get; set; }
    public RoleModel Role { get; set; }
    public IEnumerable<ProvinceShortModel>? Provinces { get; set; }
    public IEnumerable<MineShortModel>? Mines { get; set; }
}


public class UserCreateModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalCode { get; set; }
    public string? Email { get; set; }
    public string MobileNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public RoleModel Role { get; set; }
    public ICollection<ProvinceShortModel>? Provinces { get; set; }
    public IEnumerable<MineShortModel>? Mines { get; set; }
}

public class UserUpdateModel
{
    public Guid PersonCode { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalCode { get; set; }
    public string? Email { get; set; }
    public string MobileNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public RoleModel Role { get; set; }
    public ICollection<ProvinceShortModel>? Provinces { get; set; }
    public IEnumerable<MineShortModel>? Mines { get; set; }

    public bool IsActive { get; set; }
}

public class UserResetPass
{
    public required string CurrentPassword { get; set; }
    public required string NewPassword { get; set; }

    [Compare("NewPassword", ErrorMessage = "رمز عبور جدید و تاییدیه آن یکسان نمی باشد")]
    public required string ConfirmNewPassword { get; set; }
    public required string CaptchaValue { get; set; }
}