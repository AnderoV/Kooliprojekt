using Kooliprojekt.Data.Extensions;
using Moq;

namespace Kooliprojekt.UnitTests
{
    public class FakeTenantProvider : ITenantProvider
    {
        private readonly Tenant _defaultTenant;

        public FakeTenantProvider()
        {
            _defaultTenant = new Tenant
            {
                ConnectionString = "",
                DatabaseType = 4,
                Id = 1,
                Host = "localhost",
                Name = "UnitTestTenant",
                
            };
        }

        public Tenant GetTenant()
        {
            return _defaultTenant;
        }

        public Tenant[] ListTenants()
        {
            return new Tenant[] { _defaultTenant };
        }
    }
}
