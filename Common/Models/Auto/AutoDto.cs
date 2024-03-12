using DocumentFormat.OpenXml.Vml;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Common.Models;

public class AutoLastLocationModel
{
    public AutoDto? Auto { get; set; }
    public long Id { get; set; }
    public decimal? Lat { get; set; }
    public decimal? Lng { get; set; }
}
public class AutoDto
{
    public long Id { get; set; }
    public int AutoModelId { get; set; }
    public AutoModelsModel AutoModel { get; set; }
    public int CreateYear { get; set; }
    public string Pelak { get; set; }

    public int Weight { get; set; } 

    public string VIN { get; set; }
    public Guid PersonCode { get; set; }
    public PersonModel Person { get; set; }

    public long DeviceCode { get; set; }
    public long MineId { get; set; }
    public MineModel Mine { get; set; }
    public bool IsActive { get; set; }

    public DocumentDto? Document { get; set; }
    
}


