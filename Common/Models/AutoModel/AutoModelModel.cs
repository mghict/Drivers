using Moneyon.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Common.Models;

public class AutoModelsModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int AutoBrandId { get; set; }
    public string? AutoBrandName { get; set; }=string.Empty;
}
