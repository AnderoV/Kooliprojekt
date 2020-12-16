using AutoMapper;
using Kooliprojekt.Data;
using Kooliprojekt.Models.OperationModels;
using Kooliprojekt.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.Controllers
{
    public class OperationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IOperationService _operationService;

        public OperationsController(ApplicationDbContext context, IMapper mapper, IOperationService operationService)
        {
            _context = context;
            _mapper = mapper;
            _operationService = operationService;
        }

        // GET: Operations
        public async Task<IActionResult> Index()
        {
            var model = await _operationService.GetOperationListItem();
            return View(model);
        }

        // GET: Operations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _operationService.GetOperationDetailModel(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model.Result);
        }

        // GET: Operations/Create
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create(int? id)
        {
            var model = await _operationService.GetCreateOperationModel();

            return View(model.Result);

        }

        // POST: Operations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("Id,Title,Desc")] Operation operation, int CarId)
        {
            if (ModelState.IsValid)
            {
                await _operationService.CreateOperation(operation, CarId);

                return RedirectToAction(nameof(Index));
            }

            return View(operation);

        }

        // GET: Operations/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _operationService.GetEditOperationModel(id);
            if (model.Result == null)
            {
                return NotFound();
            }

            return View(model.Result);
        }

        // POST: Operations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator")]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Desc")] Operation operation)
        {
            if (id != operation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _operationService.EditOperation(id, operation);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperationExists(operation.Id))
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
            return View(operation);
        }

        // GET: Operations/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _operationService.GetDeleteOperationModel(id);
            if (model == null)
            {
                return NotFound();
            } 


            return View(model.Result);
        }

        // POST: Operations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = await _operationService.DeleteOperation(id);
            return RedirectToAction(nameof(Index));
        }

        private bool OperationExists(int id)
        {
            return _context.Operations.Any(e => e.Id == id);
        }
    }
}