using AutoMapper;
using Kooliprojekt.Data;
using Kooliprojekt.Models;
using Kooliprojekt.Models.BookingModels;

namespace Kooliprojekt.MappingProfiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, BookingEditModel>();
            CreateMap<BookingEditModel, Booking>()
                .ForMember(m => m.Car, m => m.Ignore())
                .ForMember(m => m.User, m => m.Ignore());

            CreateMap<Booking, BookingListItem>();

            CreateMap<Booking, BookingDetailsModel>();
            CreateMap<Booking, BookingDeleteModel>();
        }
    }
}
