using AutoMapper;
using Model.Models;
using Service.Modals;
using Service.Modals.Request;
using Service.Modals.Respond;
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
            CreateMap<CandleRequest, Candle>().ReverseMap()
.ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => src.ImgUrl));
            CreateMap<Candle, CandleDTO>()
            .ForMember(dest => dest.NameCategory, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<ReviewRequest, Review>().ReverseMap();
            CreateMap<ReviewDTO, Review>().ReverseMap();
            CreateMap<LoginRespond, User>().ReverseMap();
            CreateMap<LoginRequest, User>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<CateRequest, Category>().ReverseMap();

            CreateMap<Order, OrderRequestDTO>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username));
            CreateMap<Order, OrderStatusDTO>().ReverseMap();

            CreateMap<OrderItem, OrderItemRespondDto>()
            .ForMember(dest => dest.CandleName, opt => opt.MapFrom(src => src.Candle.Name));
            CreateMap<Order, OrderRespondDTO>().ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username));

        }
    }
}
