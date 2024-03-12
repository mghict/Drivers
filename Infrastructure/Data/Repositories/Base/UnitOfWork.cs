using Driver.Service.IRepositories;
using Infrastructure.Data;
using Moneyon.Common.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Infrastructure.Data.Repositories;

[AutoRegister(typeof(IUnitOfWork))]
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext _context;

    public UnitOfWork(ApplicationContext context)
    {
        _context = context;
    }

    //------------------------------------------------------------------------
    private IUserRepository _userRepository;
    public IUserRepository UserRepository => 
            _userRepository = _userRepository ?? new UserRepository(_context);


    private IMineRepository _mineRepository;
    public IMineRepository MineRepository => 
            _mineRepository = _mineRepository ?? new MineRepository(_context);


    private IProvinceRepository _provinceRepository;
    public IProvinceRepository ProvinceRepository => 
            _provinceRepository= _provinceRepository ?? new ProvinceRepository(_context);


    private ICityRepository _cityRepository;
    public ICityRepository CityRepository => 
            _cityRepository = _cityRepository ?? new CityRepository(_context);


    private IAutoBrandRepository _autoBrandRepository;
    public IAutoBrandRepository AutoBrandRepository => 
            _autoBrandRepository= _autoBrandRepository ?? new AutoBrandRepository(_context);


    private IAutoModelRepository _autoModelRepository;
    public IAutoModelRepository AutoModelRepository => 
            _autoModelRepository= _autoModelRepository ?? new AutoModelRepository(_context);


    private IAutoRepository _autoRepository;
    public IAutoRepository AutoRepository => 
            _autoRepository = _autoRepository ?? new AutoRepository(_context);


    private IPersonRepository _personRepository;
    public IPersonRepository PersonRepository => 
            _personRepository= _personRepository ?? new PersonRepository(_context);


    private IRoleRepository _roleRepository;
    public IRoleRepository RoleRepository => 
            _roleRepository = _roleRepository ?? new RoleRepository(_context);


    private IPermissionRepository _permissionRepository;
    public IPermissionRepository PermissionRepository => 
            _permissionRepository= _permissionRepository ?? new PermissionRepository(_context);


    private IRecievedWeightRepository _recievedWeightRepository;
    public IRecievedWeightRepository RecievedWeightRepository=>
            _recievedWeightRepository= _recievedWeightRepository ?? new RecievedWeightRepository(_context);


    private IRecievedSpeedAndTempratureRepository _recievedSpeedAndTempratureRepository;
    public IRecievedSpeedAndTempratureRepository RecievedSpeedAndTempratureRepository =>
            _recievedSpeedAndTempratureRepository= _recievedSpeedAndTempratureRepository ?? new RecievedSpeedAndTempratureRepository(_context);


    private IUsersTokenRepository _usersTokenRepository;
    public IUsersTokenRepository UsersTokenRepository =>
            _usersTokenRepository = _usersTokenRepository ?? new UsersTokenRepository(_context);


    private IRecievedErrorRepository _recievedErrorRepository;
    public IRecievedErrorRepository RecievedErrorRepository =>
            _recievedErrorRepository= _recievedErrorRepository?? new RecievedErrorRepository(_context);

    private IRecievedMissionRepository _recievedMissionRepository;
    public IRecievedMissionRepository RecievedMissionRepository =>
            _recievedMissionRepository= _recievedMissionRepository ?? new RecievedMissionRepository(_context);

    private IRecievedNumberRepository _recievedNumberRepository;
    public IRecievedNumberRepository RecievedNumberRepository =>
            _recievedNumberRepository = _recievedNumberRepository ?? new RecievedNumberRepository(_context);


    private IMaterialRepository _materialRepository;
    public IMaterialRepository MaterialRepository => 
            _materialRepository= _materialRepository ?? new MaterialRepository(_context);


    private IDocumentRepository _documentRepository;
    public IDocumentRepository DocumentRepository =>
            _documentRepository= _documentRepository ?? new DocumentRepository(_context);


    private IMapReverseRepository _mapReverseRepository;
    public IMapReverseRepository MapReverseRepository =>
            _mapReverseRepository = _mapReverseRepository ?? new MapReverseRepository(_context);


    //-------------------------------------------------------------
    //-------------------------------------------------------------

    public int Commit()
    {
        return _context.SaveChanges();
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync();  
    }
}
