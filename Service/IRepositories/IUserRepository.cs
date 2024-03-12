using Domain.Entities;
using Moneyon.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Service.IRepositories;

public interface IUserRepository:IGenericRelationalRepository<User,long>
{
}


