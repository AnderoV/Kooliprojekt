using AutoMapper;
using Kooliprojekt.Data;
using Kooliprojekt.Models.CarModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.MappingProfiles
{
    public class CarModelProfile : Profile
    {
        public CarModelProfile()
        {
            CreateMap<CarModel, CarModelListItemModel>();

            CreateMap<CarModel, CarModelDetailModel>();

            CreateMap<CarModel, CarModelDeleteModel>();

            CreateMap<CarModel, CarModelEditModel>();
            CreateMap<CarModelEditModel, CarModel>();

            CreateMap<CarModelCreateModel, CarModel>();

        }

    }
}
