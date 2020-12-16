using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.Data.Extensions
{
    public class Tenant
    {
        public int Id { get; set; }
        public int DatabaseType { get; set; }
        public string Host { get; set; }
        public string ConnectionString { get; set; }
        public string Name { get; set; }
        public bool UseThumbnails { get; set; }
        public bool NewCarNotification { get; set; }
        public bool test { get; set; }
    }
}
