using AutoMapper;
using Model.Models;
using Service.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapper
{
    public class MapperConfigProfile : Profile
    {
        public MapperConfigProfile() 
        {
            CreateMap<CandleDTO, Candle>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();

        }
    }
}
