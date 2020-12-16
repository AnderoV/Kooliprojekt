using AutoMapper;
using Kooliprojekt.Data;
using Kooliprojekt.Models;
using Kooliprojekt.Models.BookingModels;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.Services
{
    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BookingService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

    
        public async Task<List<BookingListItem>> GetBookingListItem(string CurrentUser)
        {
          
            var booking = await _context.Bookings.Include(i => i.Car).
                                               Include(i => i.Car.CarModel).
                                               //Include(i => i.User).
                                               //Include(i => i.User.UserName).
                                               Where(i => i.User.UserName == CurrentUser).
                                               ToListAsync();
          
            var model = _mapper.Map<List<Booking>, List<BookingListItem>>(booking);
            
            return (model);
        }

        public async Task<OperationResult<BookingDetailsModel>> GetBookingDetailModel(int? id, string CurrentUser)
        {
            var result = new OperationResult<BookingDetailsModel>();
            var booking = await _context.Bookings.Include(i => i.Car)
                .Where(i=> i.User.UserName == CurrentUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            var model = _mapper.Map<Booking,BookingDetailsModel>(booking);

            result.Result = model;
            return (result);
        }

        public async Task<OperationResult<BookingDeleteModel>> GetBookingDeleteModel(int? id, string CurrentUser)
        {
            var result = new OperationResult<BookingDeleteModel>();

            var booking = await _context.Bookings
                .Where(i => i.User.UserName == CurrentUser)
               .FirstOrDefaultAsync(m => m.Id == id);
            var model = _mapper.Map<Booking, BookingDeleteModel>(booking);

            result.Result = model;
            return (result);
        }

        public async Task<OperationResult<BookingDeleteModel>> DeleteModel(int? id, string CurrentUser)
        {
            var booking = await _context.Bookings
               // .Where(i => i.User.UserName == CurrentUser)
                .FindAsync(id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return null;
        }

        public async Task<OperationResult<BookingEditModel>> GetBookingEditModel(int? id, string CurrentUser)
        {
            var result = new OperationResult<BookingEditModel>();
            var booking = await _context.Bookings.FindAsync(id);
              //  .Where(i => i.User.UserName == CurrentUser);

            var model = _mapper.Map<Booking, BookingEditModel>(booking);
            result.Result = model;
            return (result);
        }

        public async Task<OperationResult<BookingEditModel>> EditBooking(int id, Booking bookingModel)
        {
           

            var booking = await _context.Bookings
                                       .Include(b => b.Car)
                                       .FirstOrDefaultAsync(b => b.Id == id);

            var time = (bookingModel.End - booking.Start).Value.TotalMinutes;
            float timefare = (float)time * booking.Car.TimeFare;
            var KmFare = bookingModel.Km * booking.Car.KmFare;
            booking.Price = timefare + KmFare;
            booking.Km = bookingModel.Km;
            booking.End = bookingModel.End;

            _context.Update(booking);
            await _context.SaveChangesAsync();
           
            return null;
        }

        public async Task<OperationResult<Booking>> GetStopBookingModel(int? id, string CurrentUser)
        {
            var result = new OperationResult<Booking>();
            var booking = await _context.Bookings
                                        .Include(b => b.Car)
                                        .Where(i => i.User.UserName == CurrentUser)
                                        .FirstOrDefaultAsync(b => b.Id == id);
            
            result.Result = booking;
            return (result);
        }

        public async Task<OperationResult<BookingEditModel>> StopBooking(int id, Booking bookingModel, int carId, string CurrentUser)
        {
            var booking = await _context.Bookings
                                        .Include(b => b.Car)
                                        .Where(i => i.User.UserName == CurrentUser)
                                        .FirstOrDefaultAsync(b => b.Id == id);

            var time = (bookingModel.End - booking.Start).Value.TotalMinutes;
            float timefare = (float)time * booking.Car.TimeFare;
            var KmFare = bookingModel.Km * booking.Car.KmFare;
            booking.Price = timefare + KmFare;
            booking.Km = bookingModel.Km;
            booking.End = bookingModel.End;
            booking.Pending = true;
            _context.Update(booking);
            await _context.SaveChangesAsync();

            return null;
        }

      
    }
}
