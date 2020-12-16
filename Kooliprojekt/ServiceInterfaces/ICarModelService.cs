using Kooliprojekt.Data;
using Kooliprojekt.Models.CarModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.ServiceInterfaces
{
    public interface ICarModelService
    {
        public Task<OperationResult<List<CarModelListItemModel>>> GetCarModelListItems();
        public Task<OperationResult<CarModelDetailModel>> GetCarModelDetails(int? id);
        public Task<OperationResult<CarModelDeleteModel>> GetCarDeleteModel(int? id);
        public Task<OperationResult<CarModelDeleteModel>> DeleteCarModel(int? id);
        public Task<OperationResult<CarModelEditModel>> GetCarEditModel(int? id);
        public Task<OperationResult<CarModelEditModel>> EditCarModel( CarModel carModel);
        public Task<OperationResult<CarModelCreateModel>> CreateCarModel(CarModel carModel);
    }
}
