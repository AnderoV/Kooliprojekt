using AutoMapper;
using Kooliprojekt.Data;
using Kooliprojekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.MappingProfiles
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMap<Booking, InvoiceViewModel>();
            CreateMap<PagedResult<Booking>, PagedResult<InvoiceViewModel>>();
        }
    }
}
