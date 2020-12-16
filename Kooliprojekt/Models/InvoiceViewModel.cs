using Kooliprojekt.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.Models
{
    [ExcludeFromCodeCoverage]
    public class InvoiceViewModel
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public float Km { get; set; }
        public float Price { get; set; }
    }
}
