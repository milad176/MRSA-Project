using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;
using WebAPI.Models;

namespace WebAPI.Profiles
{
    public class CitiesProfile :Profile
    {
        public CitiesProfile()
        {
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<City, CityForUpdateDto>().ReverseMap();
            CreateMap<City, CityUpdateNameDto>().ReverseMap();
        }
    }
}
