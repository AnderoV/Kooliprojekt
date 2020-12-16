using Kooliprojekt.Data.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.Data
{
    public class CarModel : BaseEntity
    {
        public string Mark { get; set; }
        public string Model { get; set; }
        public IList<Car> Cars { get; set; }
    }
}
