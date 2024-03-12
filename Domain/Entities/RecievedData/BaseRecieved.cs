using Domain.Entities;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Moneyon.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Domain.Entities;


public class BaseRecieved : Entity<long>, IAudit
{
    public long? AutoId { get; set; }
    public Auto? Auto { get; set; }

    public long DeviceCode { get; set; }

    public long RowNumber { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime SendDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get ; set ; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set ; }
}
