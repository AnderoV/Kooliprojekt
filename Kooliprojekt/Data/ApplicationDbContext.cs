using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Kooliprojekt.Models;
using Kooliprojekt.Data;
using Kooliprojekt.Data.Extensions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace Kooliprojekt.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly Tenant _tenant;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantProvider tenantProvider) : base(options)
        {
            _tenant = tenantProvider.GetTenant();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_tenant.DatabaseType == 1)
            {
                optionsBuilder.UseSqlServer(_tenant.ConnectionString);
            }
            else if (_tenant.DatabaseType == 3)
            {
                optionsBuilder.UseSqlite(_tenant.ConnectionString);
            }
            else if(_tenant.DatabaseType == 4)
            {
                optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            }
            optionsBuilder.ReplaceService<IModelCacheKeyFactory, DynamicModelCacheKeyFactory>();
        }
        /*
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var type in GetEntityTypes())
            {
                var method = SetGlobalQueryMethod.MakeGenericMethod(type);
                method.Invoke(this, new object[] { builder });
            }
        }
        */
        public int TenantId
        {
            get
            {
                if(_tenant == null)
                {
                    return -1;
                }
                return _tenant.Id;
            }
        }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Kooliprojekt.Models.InvoiceViewModel> InvoiceViewModel { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Image> Images { get; set; }
        /*
        #region EntityTypes 
        private static IList<Type> _entityTypeCache;
        private static IList<Type> GetEntityTypes()
        {
            if (_entityTypeCache != null)
            {
                return _entityTypeCache.ToList();
            }

            _entityTypeCache = (from a in GetReferencingAssemblies()
                                from t in a.DefinedTypes
                                where t.BaseType == typeof(BaseEntity)
                                select t.AsType()).ToList();
            return _entityTypeCache;
        }

        private void SetTenantsIds()

        {

            var entities = from e in ChangeTracker.Entries()

                           where e.Entity is BaseEntity &&

                           ((BaseEntity)e.Entity).TenantId == 0

                           select (BaseEntity)e.Entity;



            foreach (var entity in entities)

            {

                entity.TenantId = _tenant.Id;

            }

        }

        */
        /*
        #region DefensiveOps 

        public override int SaveChanges()

        {
            SetTenantsIds();
            ThrowIfMultipleTenants();


      
            return base.SaveChanges();

        }



        public override int SaveChanges(bool acceptAllChangesOnSuccess)

        {
            SetTenantsIds();
            ThrowIfMultipleTenants();


           
            return base.SaveChanges(acceptAllChangesOnSuccess);

        }



        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))

        {
            SetTenantsIds();
            ThrowIfMultipleTenants();


            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

        }



        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))

        {
            SetTenantsIds();
            ThrowIfMultipleTenants();


            return base.SaveChangesAsync(cancellationToken);

        }



        private void ThrowIfMultipleTenants()

        {

            var ids = (from e in ChangeTracker.Entries()

                       where e.Entity is BaseEntity

                       select ((BaseEntity)e.Entity).TenantId)

            .Distinct()

            .ToList();



            if (ids.Count == 0)

            {

                return;

            }



            if (ids.Count > 1)

            {

                throw new CrossTenantUpdateException(ids);

            }



            if (ids.First() != _tenant.Id)

            {

                throw new CrossTenantUpdateException(ids);

            }

        }

        #endregion
        private static IEnumerable<Assembly> GetReferencingAssemblies()
        {
            var assemblies = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;

            foreach (var library in dependencies)
            {
                try
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(assembly);
                }
                catch (FileNotFoundException)
                { }
            }
            return assemblies;
        }

        static readonly MethodInfo SetGlobalQueryMethod = typeof(ApplicationDbContext).GetMethods(BindingFlags.Public | BindingFlags.Instance)

        .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQuery");

        public void SetGlobalQuery<T>(ModelBuilder builder) where T : BaseEntity
        {
            builder.Entity<T>().HasKey(e => e.Id);
            builder.Entity<T>().HasQueryFilter(e => e.TenantId == _tenant.Id);
        }
        #endregion
        */
    }
}