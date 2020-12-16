using AutoMapper;
using Kooliprojekt.Data;
using Kooliprojekt.Models.CarModels;
using Kooliprojekt.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.Controllers
{
    [Authorize]
    public class CarModelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICarModelService _carModelService;

        public CarModelsController(ApplicationDbContext context, IMapper mapper, ICarModelService CarModelService)
        {
            _context = context;
            _mapper = mapper;
            _carModelService = CarModelService;
        }

        // GET: CarModels
        public async Task<IActionResult> Index()
        {
          var model=  await _carModelService.GetCarModelListItems();
            return View(model.Result);
        }

        // GET: CarModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = await _carModelService.GetCarModelDetails(id);
           
            if (model.Result == null)
            {
                return NotFound();
            }

            return View(model.Result);
        }

        // GET: CarModels/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {

            return View();
        }

        // POST: CarModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       // [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("Id,Mark,Model")] CarModel carModel)
        {
            if (ModelState.IsValid)
            {
               await _carModelService.CreateCarModel(carModel);
                return RedirectToAction(nameof(Index));
            }
            return View(carModel);
        }

        // GET: CarModels/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = await _carModelService.GetCarEditModel(id);
            if (model.Result == null)
            {
                return NotFound();
            }
            return View(model.Result);
        }

        // POST: CarModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Mark,Model")] CarModel carModel)
        {
            if (id != carModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _carModelService.EditCarModel(carModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarModelExists(carModel.Id))
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
            return View(carModel);
        }

        // GET: CarModels/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           var model = await _carModelService.GetCarDeleteModel(id);
            if (model.Result == null)
            {
                return NotFound();
            }

            return View(model.Result);
        }

        // POST: CarModels/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await  _carModelService.DeleteCarModel(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CarModelExists(int id)
        {
            return _context.CarModels.Any(e => e.Id == id);
        }
    }
}
