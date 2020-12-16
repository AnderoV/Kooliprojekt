using Kooliprojekt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.Models.BookingModels
{
    public class BookingDetailsModel
    {
        public int Id { get; set; }
        public Car car { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public float Km { get; set; }
        public float Price { get; set; }
    }
}
