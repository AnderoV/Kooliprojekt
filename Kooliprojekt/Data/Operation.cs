using Kooliprojekt.Data.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.Data
{
    public class Operation : BaseEntity
    {
       
        public string Title { get; set; }
        public string Desc { get; set; }
        public Car Car { get; set; }
    }
}
