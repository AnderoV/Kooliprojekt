using AutoMapper;
using Kooliprojekt.Data;
using Kooliprojekt.Models.CarModels;
using Kooliprojekt.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.ServiceClasses
{
    public class CarModelService : ICarModelService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CarModelService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<CarModelListItemModel>>> GetCarModelListItems()
        {
            var result = new OperationResult<List<CarModelListItemModel>>();
            var carModel = await _context.CarModels.ToListAsync();
            var model = _mapper.Map<List<CarModel>, List<CarModelListItemModel>>(carModel);

            result.Result = model;
            return (result);

        }

        public async Task<OperationResult<CarModelDetailModel>> GetCarModelDetails(int? id)
        {
            var result = new OperationResult<CarModelDetailModel>();
            var carModel = await _context.CarModels
               .FirstOrDefaultAsync(m => m.Id == id);
            var model = _mapper.Map<CarModel, CarModelDetailModel>(carModel);
            result.Result = model;
            return (result);
        }

        public async Task<OperationResult<CarModelCreateModel>> CreateCarModel(CarModel carModel)
        {
            _context.Add(carModel);
            await _context.SaveChangesAsync();
            return null;
        }
        public async Task<OperationResult<CarModelEditModel>> GetCarEditModel(int? id)
        {
                    var result = new OperationResult<CarModelEditModel>();

                    var carModel = await _context.CarModels.FindAsync(id);
                    var model = _mapper.Map<CarModel, CarModelEditModel>(carModel);
                    result.Result = model;
                    return (result);
        }
        public async Task<OperationResult<CarModelEditModel>> EditCarModel( CarModel carModel)
        {
            _context.Update(carModel);
            await _context.SaveChangesAsync();
            return null;
        }

        public async Task<OperationResult<CarModelDeleteModel>> GetCarDeleteModel(int? id)
        {
            var result = new OperationResult<CarModelDeleteModel>();

            var carModel = await _context.CarModels
               .FirstOrDefaultAsync(m => m.Id == id);
            var model = _mapper.Map<CarModel, CarModelDeleteModel>(carModel);
            result.Result = model;

            return (result);
        }

        public async Task<OperationResult<CarModelDeleteModel>> DeleteCarModel(int? id)
        {
            var carModel = await _context.CarModels.FindAsync(id);

            _context.CarModels.Remove(carModel);

            await _context.SaveChangesAsync();
            return null;
        }
    }
}
