using Kooliprojekt.Data;
using Kooliprojekt.Models;
using Kooliprojekt.Models.BookingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.Services
{
    public interface IBookingService
    {
        public Task<List<BookingListItem>> GetBookingListItem(string CurrentUser);
        public Task<OperationResult<BookingDetailsModel>> GetBookingDetailModel(int? id, string CurrentUser);
        public Task<OperationResult<BookingDeleteModel>> GetBookingDeleteModel(int? id, string CurrentUser);
        public Task<OperationResult<BookingDeleteModel>> DeleteModel(int? id, string CurrentUser);
        public Task<OperationResult<BookingEditModel>> GetBookingEditModel(int? id, string CurrentUser);
        public Task<OperationResult<BookingEditModel>> EditBooking(int id, Booking bookingModel);
        public Task<OperationResult<Booking>> GetStopBookingModel(int? id, string CurrentUser);
        public Task<OperationResult<BookingEditModel>> StopBooking(int id, Booking bookingModel, int carId, string CurrentUser);
    }
}
