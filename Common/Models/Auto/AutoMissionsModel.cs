using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Common.Models;

public class AutoMissionsModel
{
    public long Id { get; set; }
    public long AutoId { get; set; }
    public string VIN { get; set; }
    public string Pelak { get; set; }
    public long MissionCode { get; set; }
    public string DriverDisplayName { get; set; }
    public int Weight { get; set; }
    public int AutoWeight { get; set; } = 0;

    public decimal NetWeight => Weight - AutoWeight;
    public DateTime StartDate { get; set; }
    public long Type { get; set; }
    public string TypeName { get; set; }
    public DateTime? EndDate { get; set; }
    public TimeSpan? TotalDay =>  EndDate?.Subtract(StartDate) ?? null;
    public long MineId { get; set; }
    public string MineName { get; set; }
    public int ProvinceId { get; set; }
    public string ProvinceName { get; set; }
    public int CityId { get; set; }
    public string CityName { get; set; }
    public string DeviceCode { get; set; }
    public int MaterialId { get; set; }
    public string MaterialName { get; set; }
}
