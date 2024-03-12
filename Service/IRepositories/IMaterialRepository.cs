using Driver.Domain.Entities;
using Moneyon.Common.Data;

namespace Driver.Service.IRepositories;

public interface IMaterialRepository : IGenericRelationalRepository<Material, int>
{
}