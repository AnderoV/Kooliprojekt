using AutoMapper;
using Kooliprojekt.Data;
using Kooliprojekt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.Controllers
{
    [Authorize]
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICarService _CarService;
      

        public CarsController(ApplicationDbContext context, IMapper mapper, ICarService CarService)
        {
            _context = context;
            _mapper = mapper;
            _CarService = CarService;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            var model = await _CarService.GetCarListItems();
            return View(model.Result);
        }


        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _CarService.GetCarDetails(id);


            if (model.Result.CarModel.Cars == null)
            {
                return NotFound();
            }


            return View(model.Result);
        }

        // GET: Cars/Create
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create()
        {
            var model = await _CarService.GetCarCreateModel();

            return View(model.Result);
        }

        // POST: Cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LicencePlate,KmFare,TimeFare,Picture")] Car car, int CarModelId)
        {
            if (ModelState.IsValid)
            {
                await _CarService.CreateCar(car, CarModelId);
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _CarService.GetCarEditModel(id);
            if (model.Result.CarModel == null)
            {
                return NotFound();
            }



            return View(model.Result);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       // [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LicencePlate,KmFare,TimeFare,Picture,CarModel")] Car car, int CarModelId)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _CarService.EditCar(id, car, CarModelId);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
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
            return View(car);
        }

        // GET: Cars/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _CarService.GetCarDeleteModel(id);

            return View(model.Result);
        }

        // POST: Cars/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _CarService.DeleteCar(id);

            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
