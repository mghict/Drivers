using Driver.Domain.Entities;
using Moneyon.Common.Data;

namespace Driver.Service.IRepositories;

public interface IPermissionRepository : IGenericRelationalRepository<Permission, int>
{
}


