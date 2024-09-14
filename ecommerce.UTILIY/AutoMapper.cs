using AutoMapper;
using ecommerce.DTO.Registration;
using ecommerce.MODEL;

namespace ecommerce.UTILIY
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Mapeo para registrar Usuario
            CreateMap<User, RegisterUserDto>().ReverseMap();

            // Mapeo para registrar comprador
            CreateMap<Buyer, RegisterBuyerDto>().ReverseMap();

            // Mapeo para registrar vendedor
            CreateMap<Seller, RegisterSellerDto>().ReverseMap();
        }
    }
}
