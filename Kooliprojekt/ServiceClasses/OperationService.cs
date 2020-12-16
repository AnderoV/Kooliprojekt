using AutoMapper;
using Kooliprojekt.Data;
using Kooliprojekt.Models.OperationModels;
using Kooliprojekt.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.ServiceClasses
{
    public class OperationService : 
        IOperationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OperationService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<OperationListItemModel>> GetOperationListItem()
        {
            var operation = await _context.Operations.Include(i => i.Car).Include(i => i.Car.CarModel).ToListAsync();
            var model = _mapper.Map<List<Operation>, List<OperationListItemModel>>(operation);

            return model;
        }
        public async Task<OperationResult<OperationDetailsModel>> GetOperationDetailModel(int? id)
        {
            var result = new OperationResult<OperationDetailsModel>();
            var operation = await _context.Operations.Include(i => i.Car).Include(i => i.Car.CarModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (operation != null)
            { 
            var model = _mapper.Map<Operation, OperationDetailsModel>(operation);
            result.Result = model;
            }

            return result;
        }
        public async Task<OperationResult<OperationCreateModel>> GetCreateOperationModel()
        {
            var result = new OperationResult<OperationCreateModel>();
            var operation = new Operation();
            var model = _mapper.Map<Operation, OperationCreateModel>(operation);
            model.Car = await _context.Cars.Select(Car => new SelectListItem
            {
                Text = Car.LicencePlate,    
                Value = Car.Id.ToString()
            }).ToListAsync();

            result.Result = model;

            return result;
        }
        public async Task<OperationResult<OperationCreateModel>> CreateOperation(Operation operation, int CarId)
        {
            var car = await _context.Cars.FindAsync(CarId);
            operation.Car = car;
            _context.Add(operation);
            await _context.SaveChangesAsync();
            return null;
        }
        public async Task<OperationResult<OperationEditModel>> GetEditOperationModel(int? id)
        {
            var result = new OperationResult<OperationEditModel>();
            var operation = await _context.Operations.FindAsync(id);

            if (operation != null)
            {
                var model = _mapper.Map<Operation, OperationEditModel>(operation);
                result.Result = model;
            }

            return result;
        }
        public async Task<OperationResult<OperationEditModel>> EditOperation(int id, Operation operation)
        {
            _context.Update(operation);
            await _context.SaveChangesAsync();
            return null;
        }
        public async Task<OperationResult<OperationDeleteModel>> GetDeleteOperationModel(int? id)
        {
            var result = new OperationResult<OperationDeleteModel>();
            var operation = await _context.Operations
                .FirstOrDefaultAsync(m => m.Id == id); 

            if (operation != null)
            { 
            var model = _mapper.Map<Operation, OperationDeleteModel>(operation);
            result.Result = model;
            }

            return result;
        }
        public async Task<OperationResult<OperationDeleteModel>> DeleteOperation(int? id)
        {
            var operation = await _context.Operations.FindAsync(id);
            _context.Operations.Remove(operation);
            await _context.SaveChangesAsync();
            return null;
        }

    }
}