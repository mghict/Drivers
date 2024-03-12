namespace Driver.Common.Models;

public class AutoDetailsDto
{
    public long DeviceCode { get; set; }
    public string AutoBrandName { get; set; }
    public string AutoModelName { get; set; }
    public int CreateYear { get; set; }
    public string Pelak { get; set; }
    public string VIN { get; set; }
    public int Weight { get; set; } 
    public long MineCode { get; set; }
    public string MineName { get; set; }
    public string MineProvinceName { get; set; }
    public string MineCityName { get; set; }
    public string MineAddress { get; set; }

    public bool AutoIsActive { get; set; }
    public bool DriverIsActive { get; set; }
    public Guid DriverPersonCode { get; set; }
    public string DriverFirstName { get; set; }
    public string DriverLastName { get; set; }
    public string DriverNationalCode { get; set; }
    public string DriverMobileNumber { get; set; }
    public DateTime DriverBirthDate { get; set; }
    public string DriverBirthDateShamsi { get; set; }
    public string? DriverPic { get; set; }
    public string? AutoPic { get; set; }
}


