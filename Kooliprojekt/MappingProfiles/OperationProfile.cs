using AutoMapper;
using Kooliprojekt.Data;
using Kooliprojekt.Models.OperationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.MappingProfiles
{
    public class OperationProfiles : Profile
    {
        public OperationProfiles()
        {
            CreateMap<Operation, OperationListItemModel>();

            CreateMap<Operation, OperationDetailsModel>();

            CreateMap<Operation, OperationEditModel>();

            CreateMap<Operation, OperationCreateModel>();

            CreateMap<Operation, OperationDeleteModel>();
        }
    }
}
