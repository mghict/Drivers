using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Common.Models;

public record MineCreateModel(string Name, LocationCreateModel Location, string Address, ICollection<int>? MaterialIds = null,long MineCode=0);
