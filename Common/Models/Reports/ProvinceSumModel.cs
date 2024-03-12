using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Common.Models.Reports
{
    public class ProvinceSumModel
    {
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public int MineCount { get; set; } = 0;
        public int AutoCount { get; set; } = 0;
        
    }

    public class ProvinceMineSumModel
    {
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public long MineId { get; set; }
        public string MineName { get; set; }
        public long MineCode { get; set; }
        public int AutoCount { get; set; } = 0;
        public List<string>? MaterialNames { get; set; }

    }

    public class CitySumModel
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int MineCount { get; set; } = 0;
        public int AutoCount { get; set; } = 0;
        public decimal TotalWeight { get; set; } = 0;
        public double TotalDays { get; set; }
        public double AvrageDays { get; set; }
    }

    public class MineSumModel
    {
        public long MineId { get; set; }
        public string MineName { get; set; }
        public int AutoCount { get; set; } = 0;

    }

    public class MineMaterialSumModel
    {
        public long MaterialId { get; set; }
        public string MaterialName { get; set; }
        public long Weight { get; set; }

    }
}
