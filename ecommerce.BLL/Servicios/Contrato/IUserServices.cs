using ecommerce.DTO.Registration;

namespace ecommerce.BLL.Servicios.Contrato
{
    public interface IUserServices
    {
        Task<RegisterUserDto> CreateUser(RegisterUserDto model);
    }
}
