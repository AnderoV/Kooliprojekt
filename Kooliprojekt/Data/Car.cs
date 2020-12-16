using Kooliprojekt.Data.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.Data
{
    public class Car : BaseEntity
    {
        public CarModel CarModel { get; set; }
        public string LicencePlate { get; set; }
        public float KmFare { get; set; }
        public float TimeFare { get; set; }
        public IList<Image> Pictures { get; set; }
        public IList<Booking> Bookings { get; set; }
        public IList<Operation> Operations { get; set; }
    }
}
