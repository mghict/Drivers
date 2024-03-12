using Driver.Domain.Entities;
using Moneyon.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Service.IRepositories;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get;}
    public IMineRepository MineRepository { get;}
    public IProvinceRepository ProvinceRepository { get;}
    public ICityRepository CityRepository { get;}
    public IAutoBrandRepository AutoBrandRepository { get;}
    public IAutoModelRepository AutoModelRepository { get;}
    public IAutoRepository AutoRepository { get;}
    public IPersonRepository PersonRepository { get;}
    public IRoleRepository RoleRepository { get;}
    public IPermissionRepository PermissionRepository { get;}

    public IRecievedWeightRepository RecievedWeightRepository { get;}
    public IRecievedSpeedAndTempratureRepository RecievedSpeedAndTempratureRepository { get; }
    public IRecievedErrorRepository RecievedErrorRepository { get; }
    public IRecievedMissionRepository RecievedMissionRepository { get; }
    public IRecievedNumberRepository RecievedNumberRepository { get; }

    public IMaterialRepository MaterialRepository { get; }
    public IUsersTokenRepository UsersTokenRepository { get; }

    public IDocumentRepository DocumentRepository { get; }

    public IMapReverseRepository MapReverseRepository { get; }
    //-------------------------------------------------------------
    //-------------------------------------------------------------
    Task CommitAsync(CancellationToken cancellationToken = default);
    int Commit();
}
