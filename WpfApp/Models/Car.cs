using Kooliprojekt.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WpfApp.Models
{
    public class Car
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
