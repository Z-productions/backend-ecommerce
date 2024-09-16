using ecommerce.DTO.Common;
using ecommerce.DTO.Registration;

namespace ecommerce.BLL.Servicios.Contrato
{
    public interface IUserServices
    {
        // Registrar Usuario
        Task<RegisterUserDto> CreateUser(RegisterUserDto model);
    }
}
