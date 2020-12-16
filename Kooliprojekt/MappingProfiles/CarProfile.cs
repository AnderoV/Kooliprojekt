using AutoMapper;
using Kooliprojekt.Data;
using Kooliprojekt.Models.CarModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.MappingProfiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarListItemModel>();

            CreateMap<Car, CarDetailsModel>();

            CreateMap<Car, CarDeleteModel>();

            CreateMap<Car, CarEditModel>();

            CreateMap<Car, CarCreateModel>();
        }

    }
}
