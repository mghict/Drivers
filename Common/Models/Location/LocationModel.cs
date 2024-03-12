using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Common.Models;

public class LocationModel
{
    public long Id { get; set; }
    public int CityId { get; set; }
    public string CityName { get; set; }
    public int ProvinceId { get; set; }
    public string ProvinceName { get; set; }
    public string Name { get; set; }
    public decimal lat { get; set; }
    public decimal lng { get; set; }

}

public class LocationCreateModel
{
    public int CityId { get; set; }
    public decimal lat { get; set; }
    public decimal lng { get; set; }

}