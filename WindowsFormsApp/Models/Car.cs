using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WindowsFormsApp.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public string CarModel { get; set; }
        public string LicencePlate { get; set; }
        public float KmFare { get; set; }
        public float TimeFare { get; set; }
        public string Picture { get; set; }
    }
}
