using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.Models.BookingModels
{
    [ExcludeFromCodeCoverage]
    public class BookingEditModel
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public float Km { get; set; }
        public float Price { get; set; }
    }
}
