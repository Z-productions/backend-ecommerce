using AutoMapper;
using ecommerce.BLL.ExtensionMetodos;
using ecommerce.BLL.Servicios.Contrato;
using ecommerce.DAL.Repository.Contrato;
using ecommerce.DTO.Registration;
using ecommerce.MODEL;

namespace ecommerce.BLL.Servicios
{
    public class UserService : IUserServices
    {
        private readonly IGenericRepository<User> userRepository;
        private readonly IMapper mapper;

        public UserService(IGenericRepository<User> userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<RegisterUserDto> CreateUser(RegisterUserDto model)
        {
            try
            {
                // Validar el DTO usando el método de extensión (campos vacíos)
                model.ValidateDto();

                // Validar el correo electrónico
                if (!string.IsNullOrWhiteSpace(model.Email) && !model.Email.IsValidEmail())
                {
                    throw new ArgumentException("El correo electrónico no es válido.");
                }

                // Validar la URL de la imagen
                if (!string.IsNullOrWhiteSpace(model.ImageUrl) && !model.ImageUrl.IsValidUrl())
                {
                    throw new ArgumentException("La URL de la imagen no es válida.");
                }

                // Encriptar la contraseña usando el método de extensión
                model.Password = model.Password.EncryptPassword();

                // Mapear el DTO al modelo
                var userCrate = await userRepository.AddAsync(mapper.Map<User>(model));

                if (userCrate.IdUser == 0)
                    throw new TaskCanceledException("No se puede crear el usuario");

                return mapper.Map<RegisterUserDto>(userCrate);

            }
            catch (TaskCanceledException ex)
            {
                throw new ApplicationException("Error al crear la cuenta: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al crear la cuenta", ex);
            }
        }
    }
}
