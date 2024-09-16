using ecommerce.DTO.Common;
using ecommerce.DTO.Login;
using ecommerce.DTO.Registration;

namespace ecommerce.BLL.Servicios.Contrato
{
    public interface IUserServices
    {
        // Registrar Usuario
        Task<RegisterUserDto> CreateUser(RegisterUserDto model);

        // Obtener todos los usuarios
        Task<List<UserDto>> GetUserDtos();

        // Obtener usuarios por nombre de inicio de sesión
        Task<List<RegisterUserDto>> GetLoginName(string name);

        // Realizar inicio de sesión
        Task<LoginResponseDto> LoginDtos(LoginDto model);

        // Eliminar usuario
        Task<bool> DeleteUser(long userId);

        // Actualizar usuario
        Task<UserDto> UpdateUser(UserDto model);

    }
}
