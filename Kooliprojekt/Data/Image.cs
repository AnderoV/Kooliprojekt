using Kooliprojekt.Data.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.Data
{
    public class Image: BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }

    }
}
