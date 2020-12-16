using Kooliprojekt.Data.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.Data
{
    public class Booking : BaseEntity
    {
        public Car Car { get; set; }
        public IdentityUser User { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public float Km { get; set; }
        public float Price { get; set; }
        public bool? Pending { get; set; }
    }
}
