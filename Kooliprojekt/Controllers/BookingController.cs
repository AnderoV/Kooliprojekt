using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kooliprojekt.Data;
using AutoMapper;
using Kooliprojekt.Models;
using Microsoft.AspNetCore.Authorization;
using Kooliprojekt.Models.BookingModels;
using Kooliprojekt.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Kooliprojekt.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IBookingService _bookingService;

        public BookingsController(ApplicationDbContext context, IMapper mapper,IBookingService bookingService)
        {
            _context = context;
            _mapper = mapper;
            _bookingService = bookingService;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var currentUser = User.Identity.Name;
            // Anna teenusesse kaasa ja lisa where tingimusse
            var model = await _bookingService.GetBookingListItem(currentUser);
            return View(model);
        }

        // GET: Bookings/Details/5
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUser = User.Identity.Name;
            // Anna teenusesse kaasa ja lisa where tingimusse
            var model = await _bookingService.GetBookingDetailModel(id, currentUser);
            if (model == null)
            {
                return NotFound();
            }
            

            return View(model.Result);
        }



        // GET: Bookings/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var currentUser = User.Identity.Name;
        //    // Anna teenusesse kaasa ja lisa where tingimusse kui serviceni jõuate
        //    var booking = await _bookingService.GetBookingEditModel(id,currentUser);
        //    if (booking == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(booking.Result);
        //}

        //// POST: Bookings/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Start,End,Km,Price,Pending")] Booking bookingModel)
        //{
        //    if (id != bookingModel.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var currentUser = User.Identity.Name;
        //            // Anna teenusesse kaasa ja lisa where tingimusse
        //            await _bookingService.EditBooking(id, bookingModel);

        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!BookingExists(bookingModel.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(bookingModel);
        //}


        public async Task<IActionResult> StopBooking(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUser = User.Identity.Name;
            // Anna teenusesse kaasa ja lisa where tingimusse
            var model = await _bookingService.GetStopBookingModel(id, currentUser);
            if (model.Result == null)
            {
                return NotFound();
            }
            return View(model.Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StopBooking(int id, [Bind("Id,Start,End,Km,Price,Pending")] Booking bookingModel, int carId)
        {
            if (id != bookingModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var currentUser = User.Identity.Name;
                    // Anna teenusesse kaasa ja lisa where tingimusse
                    await _bookingService.StopBooking(id, bookingModel, carId, currentUser);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bookingModel);
        }

        public IActionResult Book(int id)
        {
            var model = new BookingEditModel();
            model.CarId = id;

            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(BookingEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

          

            var currentUser = User.Identity.Name;

            var booking = _mapper.Map<Booking>(model);
            booking.Start = DateTime.Now;
            booking.Car = await _context.Cars.FindAsync(model.CarId);
            booking.User = await _context.Users.Where(u => u.UserName == User.Identity.Name)
                                               .FirstOrDefaultAsync();
            booking.User.UserName = currentUser;
            _context.Add(booking);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));


        }


        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUser = User.Identity.Name;
            // Anna teenusesse kaasa ja lisa where tingimusse
            var model = await _bookingService.GetBookingDeleteModel(id, currentUser);
            if (model == null)
            {
                return NotFound();
            }

            return View(model.Result);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var currentUser = User.Identity.Name;
            // Anna teenusesse kaasa ja lisa where tingimusse
            await _bookingService.DeleteModel(id, currentUser);
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}
