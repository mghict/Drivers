namespace Driver.Common.Models;

public class AutoTransportModel
{
    public DateTime SendDate { get; set; }
    public decimal Lat { get; set; }
    public decimal Lng { get; set; }
    public decimal Temprature { get; set; }
    public decimal Speed { get; set; }
    public long MissionCode { get; set; }
    public long DeviceCode { get; set; }
}
