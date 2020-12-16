using Kooliprojekt.Data;
using Kooliprojekt.Models.CarModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.Services
{
    public interface ICarService
    {
        // Task<List<CarListItemModel>> GetCarListItems();
        public Task<OperationResult<List<CarListItemModel>>> GetCarListItems();
        public Task<OperationResult<CarDetailsModel>> GetCarDetails(int? id);
        public Task<OperationResult<CarDeleteModel>> GetCarDeleteModel(int? id);
        public Task<OperationResult<CarDeleteModel>> DeleteCar(int? id);
        public Task<OperationResult<CarEditModel>> GetCarEditModel(int? id);
        public Task<OperationResult<CarEditModel>> EditCar(int? id, Car car, int CarModelId);
        public Task<OperationResult<CarCreateModel>> GetCarCreateModel();
        public Task<OperationResult<CarCreateModel>> CreateCar(Car car, int CarModelId);

    }
}
