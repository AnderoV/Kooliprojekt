using AutoMapper;
using Kooliprojekt.Data;
using Kooliprojekt.Models.CarModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.Services
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CarService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<CarListItemModel>>> GetCarListItems()
        {
            var result = new OperationResult<List<CarListItemModel>>();
            var car = await _context.Cars.Include(i => i.CarModel).Include(i => i.Pictures).ToListAsync();
            var model = _mapper.Map<List<Car>, List<CarListItemModel>>(car);
            result.Result = model;


            return (result);
        }

        public async Task<OperationResult<CarDetailsModel>> GetCarDetails(int? id)
        {

            var result = new OperationResult<CarDetailsModel>();
            var car = await _context.Cars.Include(i => i.CarModel).Include(i => i.Pictures)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            var model = _mapper.Map<Car, CarDetailsModel>(car);

            result.Result = model;

            return (result);

        }

        public async Task<OperationResult<CarDeleteModel>> GetCarDeleteModel(int? id)
        {
            var result = new OperationResult<CarDeleteModel>();
            var car = await _context.Cars
               .FirstOrDefaultAsync(m => m.Id == id);

            var model = _mapper.Map<Car, CarDeleteModel>(car);

            result.Result = model;

            return (result);
        }
        public async Task<OperationResult<CarDeleteModel>> DeleteCar(int? id)
        {

            var car = await _context.Cars.FindAsync(id);

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return null;

        }

        public async Task<OperationResult<CarEditModel>> GetCarEditModel(int? id)
        {
            var result = new OperationResult<CarEditModel>();

            var car = await _context.Cars.Include(i => i.Pictures).FirstOrDefaultAsync(m => m.Id == id);


            var model = _mapper.Map<Car, CarEditModel>(car);

            model.CarModel = await _context.CarModels.Select(CarModel => new SelectListItem
            {
                Text = CarModel.Model,
                Value = CarModel.Id.ToString()

            }).ToListAsync();

            result.Result = model;

            return (result);
        }

        public async Task<OperationResult<CarEditModel>> EditCar(int? id, Car car, int CarModelId)
        {
            var carmodel = await _context.CarModels.FindAsync(CarModelId);
            car.CarModel = carmodel;
            _context.Update(car);
            await _context.SaveChangesAsync();
            return null;
        }

        public async Task<OperationResult<CarCreateModel>> GetCarCreateModel()
        {
            var result = new OperationResult<CarCreateModel>();

            var car = new Car();
            var model = _mapper.Map<Car, CarCreateModel>(car);
            model.CarModel = await _context.CarModels.Select(CarModel => new SelectListItem
            {
                Text = CarModel.Model,
                Value = CarModel.Id.ToString()

            }).ToListAsync();

            result.Result = model;
            return (result);
        }

        public async Task<OperationResult<CarCreateModel>> CreateCar(Car car, int CarModelId)
        {
            var carmodel = await _context.CarModels.FindAsync(CarModelId);
            car.CarModel = carmodel;
            _context.Update(car);
            await _context.SaveChangesAsync();
            return (null);
        }
    }
}
