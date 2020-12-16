using Kooliprojekt.Data;
using Kooliprojekt.Models.OperationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.ServiceInterfaces
{
    public interface IOperationService
    {
        public Task<List<OperationListItemModel>> GetOperationListItem();
        public Task<OperationResult<OperationDetailsModel>> GetOperationDetailModel(int? id);
        public Task<OperationResult<OperationCreateModel>> GetCreateOperationModel();
        public Task<OperationResult<OperationCreateModel>> CreateOperation(Operation operation, int CarId);
        public Task<OperationResult<OperationEditModel>> GetEditOperationModel(int? id);
        public Task<OperationResult<OperationEditModel>> EditOperation(int id, Operation operation);
        public Task<OperationResult<OperationDeleteModel>> GetDeleteOperationModel(int? id);
        public Task<OperationResult<OperationDeleteModel>> DeleteOperation(int? id);

    }
}
