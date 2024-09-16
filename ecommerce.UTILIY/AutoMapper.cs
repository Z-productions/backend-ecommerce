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
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.IdUser))  // Mapear UserId
                .ForMember(dest => dest.DocumentTypeId, opt => opt.MapFrom(src => src.DocumentType.Id))  // Mapear DocumentTypeId
                .ReverseMap();

            // Mapeo para registrar vendedor
            CreateMap<Seller, RegisterSellerDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.IdUser))  // Mapear UserId
                .ForMember(dest => dest.DocumentTypeId, opt => opt.MapFrom(src => src.DocumentType.Id))  // Mapear DocumentTypeId
                .ReverseMap();

            // Mapeo para registrar tipo de documento
            CreateMap<DocumentType, DocumentTypeDto>().ReverseMap();
        }
    }
}
