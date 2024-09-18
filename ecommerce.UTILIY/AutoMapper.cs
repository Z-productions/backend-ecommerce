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

            // Mapeo para mostrar usuario
            CreateMap<User, UserDto>().ReverseMap();

            // Mapeo para registrar comprador
            CreateMap<Buyer, RegisterBuyerDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))  // Mapeo de UserId
                .ForMember(dest => dest.DocumentTypeId, opt => opt.MapFrom(src => src.DocumentTypeId))  // Mapeo de DocumentTypeId
                .ReverseMap();

            // Mapeo para registrar vendedor
            CreateMap<Seller, RegisterSellerDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.IdUser))  // Mapeo de UserId
                .ForMember(dest => dest.DocumentTypeId, opt => opt.MapFrom(src => src.DocumentTypeId))  // Mapeo de DocumentTypeId
                .ReverseMap();

            // Mapeo para registrar tipo de documento
            CreateMap<DocumentType, DocumentTypeDto>().ReverseMap();
        }
    }
}
