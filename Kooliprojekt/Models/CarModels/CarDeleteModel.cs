using Kooliprojekt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.Models.CarModels
{
    public class CarDeleteModel
    {
        public int Id { get; set; }
        public CarModel CarModel { get; set; }
        public string LicencePlate { get; set; }
        public float KmFare { get; set; }
        public float TimeFare { get; set; }
      //  public List<string> Picture { get; set; }
    }
}
