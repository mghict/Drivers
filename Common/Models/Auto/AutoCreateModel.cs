namespace Driver.Common.Models;

public class AutoCreateModel
{
    public int AutoModelId { get; set; }
    public int CreateYear { get; set; }
    public string Pelak { get; set; }

    public int Weight { get; set; } = 0;

    public string VIN { get; set; }
    public Guid PersonCode { get; set; }
    public Mineshort Mine { get; set; }
    public long DeviceCode { get; set; }
}

public class Mineshort
{
    public long Id { get; set; }
}