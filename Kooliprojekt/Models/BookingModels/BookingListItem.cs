using Kooliprojekt.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.Models.BookingModels
{
    [ExcludeFromCodeCoverage]
    public class BookingListItem
    {
        public int Id { get; set; }
        public Car Car { get; set; }
        public IdentityUser User { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public float Km { get; set; }
        public float Price { get; set; }
        public bool? Pending { get; set; }
        // Muud auto omadused listis näitamiseks
    }
}
