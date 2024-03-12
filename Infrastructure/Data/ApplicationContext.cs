using Domain.Entities;
using Domain.Interface;
using Driver.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationContext : DbContext 
    {
        public DbSet<Person> Persons { get ; set ; }
        public DbSet<User> Users { get ; set ; }
        public DbSet<Role> Roles { get ; set ; }
        public DbSet<Country> Countrys { get ; set ; }
        public DbSet<Province> Provinces { get ; set; }
        public DbSet<City> Citys { get ; set ; }
        public DbSet<Location> Locations { get ; set; }
        public DbSet<Mine> Mines { get ; set ; }
        public DbSet<AutoBrand> AutoBrands { get ; set ; }
        public DbSet<AutoModel> AutoModels { get ; set ; }
        public DbSet<Auto> Autos { get ; set ; }
        public DbSet<RecievedError> RecievedErrors { get; set; }
        public DbSet<RecievedMission> RecievedMissions { get; set; }
        public DbSet<RecievedNumber> RecievedNumbers { get; set; }
        public DbSet<RecievedSpeedAndTemprature> RecievedSpeedAndTempratures { get; set; }
        public DbSet<RecievedWeight> RecievedWeights { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UsersToken> UsersTokens { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<MapReverse> MapReverses { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentContent> DocumentContents { get; set; }

        public ApplicationContext() : base()
        {
        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.EnableSensitiveDataLogging();

            if (!options.IsConfigured)
            {
                options.UseSqlServer("Data Source=185.10.75.52;Initial Catalog=dadarfonon_ir_drivers;User Id=dadarfonon_ir_dadarfonon.ir;Password= Mgh@994500522646;MultipleActiveResultSets=True;TrustServerCertificate=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
            base.OnModelCreating(modelBuilder);
            //SeedDatabase.Initialize(modelBuilder);
        }
        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AuditHandel();
            //UserChanged();
            return await base.SaveChangesAsync(cancellationToken);
        }
        public override int SaveChanges()
        {
            AuditHandel();
            //UserChanged();
            return base.SaveChanges();
        }

        private void AuditHandel()
        {
            var entities = ChangeTracker.Entries().Where(e => e.Entity is IAudit && (e.State == EntityState.Added || e.State == EntityState.Modified));
            foreach (var entityEntry in entities)
            {
                var entity = (IAudit)entityEntry.Entity;
                DateTime dateTime = DateTime.UtcNow;
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        entity.ModifiedOn = dateTime;
                        entity.CreatedOn = dateTime;
                        break;
                    case EntityState.Modified:
                        entity.ModifiedOn = dateTime;
                        break;
                }
            }
        }
        private void UserChanged()
        {
            var entities = ChangeTracker.Entries().Where(e => e.Entity is User && (e.State == EntityState.Added || e.State == EntityState.Modified));
            foreach (var entityEntry in entities)
            {
                var entity = (User)entityEntry.Entity;
                DateTime dateTime = DateTime.UtcNow;
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        entity.LastChanged = dateTime;
                        break;
                    case EntityState.Modified:
                        entity.LastChanged = dateTime;
                        break;
                }
            }
        }

    }
}
