using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.Models.CarModels
{
    public class CarCreateModel
    {
        public int Id { get; set; }
        public IList<SelectListItem> CarModel { get; set; }
        public int CarModelId { get; set; }
        public string LicencePlate { get; set; }
        public float KmFare { get; set; }
        public float TimeFare { get; set; }
      //  public List<string> Picture { get; set; }
    }
}
