using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.Data.Extensions
{
    public interface ITenantProvider
    {
        Tenant GetTenant();
        Tenant[] ListTenants();
    }
}
