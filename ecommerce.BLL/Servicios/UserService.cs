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


        //  Creando el Usuario
        public async Task<RegisterUserDto> CreateUser(RegisterUserDto model)
        {
            try
            {
                // Validar el DTO usando el método de extensión (campos vacíos)
                model.ValidateDto();

                // Validar el correo electrónico
                if (!string.IsNullOrWhiteSpace(model.Email) && !model.Email.IsValidEmail())
                {
                    throw new ArgumentException("El correo electrónico ingresado no es válido. Por favor, ingrese un correo electrónico correcto.");
                }

                // Validar la URL de la imagen
                if (!string.IsNullOrWhiteSpace(model.ImageUrl) && !model.ImageUrl.IsValidUrl())
                {
                    throw new ArgumentException("La URL de la imagen proporcionada no es válida. Por favor, verifique que sea una dirección URL correcta.");
                }

                // Encriptar la contraseña usando el método de extensión
                model.Password = model.Password.EncryptPassword();
                
                // Cuenta activada
                model.Activated = true;

                // Mapear el DTO al modelo de usuario y agregarlo
                var userCreated = await userRepository.AddAsync(mapper.Map<User>(model));

                // Verificar si la creación fue exitosa
                if (userCreated.IdUser == 0)
                    throw new TaskCanceledException("No se pudo crear el usuario. Por favor, inténtelo de nuevo.");

                // Retornar el DTO del usuario creado
                return mapper.Map<RegisterUserDto>(userCreated);
            }
            catch (TaskCanceledException ex)
            {
                // Mensaje específico para errores de cancelación de tareas
                throw new ApplicationException("Ocurrió un error al crear la cuenta: " + ex.Message, ex);
            }
            catch (ArgumentException ex)
            {
                // Mensaje específico para errores de validación
                throw new ApplicationException("Error de validación: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Mensaje genérico para cualquier otro tipo de excepción
                throw new ApplicationException("Ocurrió un error inesperado al crear la cuenta. Por favor, intente más tarde. " + ex.Message);
            }



        }
    }
}
