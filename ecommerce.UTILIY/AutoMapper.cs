using AutoMapper;
using ecommerce.DTO.Common;
using ecommerce.DTO.Registration;
using ecommerce.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.UTILIY
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Mapeos para Producto
            CreateMap<Product, ProductDto>().ReverseMap();

            // Mapeos para Detalle
            CreateMap<Detail, DetailDto>().ReverseMap();

            // Mapeos para Remuneración
            CreateMap<Remuneration, RemunerationDto>().ReverseMap();

            // Mapeos para Pago
            CreateMap<Payment, PaymentDto>().ReverseMap();

            // Mapeos para Orden
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details))
                .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.Payment))
                .ForMember(dest => dest.Remuneration, opt => opt.MapFrom(src => src.Remuneration))
                .ReverseMap();

            // Mapeos para Comprador
            CreateMap<Buyer, BuyerDto>().ReverseMap();

            // Mapeos para Vendedor
            CreateMap<Seller, SellerDto>().ReverseMap();

            // Mapeo para registrar un usuario (comprador o vendedor)
            CreateMap<RegisterUserDto, User>()
            .ForMember(dest => dest.Buyer, opt => opt.MapFrom(src => src.UserType == UserType.Buyer ? src.Buyer : null))
            .ForMember(dest => dest.Seller, opt => opt.MapFrom(src => src.UserType == UserType.Seller ? src.Seller : null));

            // Mapeo para registrar comprador
            CreateMap<RegisterBuyerDto, Buyer>();

            // Mapeo para registrar vendedor
            CreateMap<RegisterSellerDto, Seller>();
        }
    }
}
