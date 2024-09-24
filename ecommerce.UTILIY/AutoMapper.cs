using AutoMapper;
using ecommerce.DTO.Common;
using ecommerce.DTO.Registration;
using ecommerce.MODEL;

namespace ecommerce.UTILIY
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Mapeo para registrar usuario
            CreateMap<User, RegisterUserDto>().ReverseMap();

            // Mapeo para registrar comprador
            CreateMap<Buyer, RegisterBuyerDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))  // Mapeo de UserId
                .ForMember(dest => dest.DocumentTypeId, opt => opt.MapFrom(src => src.DocumentTypeId))  // Mapeo de DocumentTypeId
                .ReverseMap();

            // Mapeo para registrar vendedor
            CreateMap<Seller, RegisterSellerDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))  // Mapeo de UserId
                .ForMember(dest => dest.DocumentTypeId, opt => opt.MapFrom(src => src.DocumentTypeId))  // Mapeo de DocumentTypeId
                .ReverseMap();

            // Mapeo para mostrar Dto
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Buyer, BuyerDto>().ReverseMap();
            CreateMap<Seller, SellerDto>().ReverseMap();
            CreateMap<DocumentType, DocumentTypeDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();


            // Mapeo para traer el numero del docuemnto del Vendedor
            CreateMap<Seller, SellerWithUserDto>()
                .ForMember(dest => dest.UserDto, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.SellerDto, opt => opt.MapFrom(src => src));

            // Mapeo para registrar productos
            CreateMap<Product, RegisterProduct>()
                .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.SellerId))  // Mapeo de Vendedor
                .ReverseMap();
        }
    }
}
