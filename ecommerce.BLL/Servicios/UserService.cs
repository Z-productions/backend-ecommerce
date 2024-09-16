using AutoMapper;
using ecommerce.BLL.ExtensionMetodos;
using ecommerce.BLL.Servicios.Contrato;
using ecommerce.BLL.Servicios.Contrato.JWT;
using ecommerce.DAL.Repository.Contrato;
using ecommerce.DTO.Common;
using ecommerce.DTO.Login;
using ecommerce.DTO.Registration;
using ecommerce.MODEL;

namespace ecommerce.BLL.Servicios
{
    public class UserService : IUserServices
    {
        private readonly IGenericRepository<User> userRepository;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public UserService(IGenericRepository<User> userRepository, ITokenService tokenService, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.tokenService = tokenService;
            this.mapper = mapper;
        }

        //  Creando el Usuario
        public async Task<RegisterUserDto> CreateUser(RegisterUserDto model)
        {
            try
            {
                // Validar el DTO usando el método de extensión (campos vacíos)
                model.ValidateDto("ImageUrl", "Activated");

                // Validar el correo electrónico
                if (!string.IsNullOrWhiteSpace(model.Email) && !model.Email.IsValidEmail())
                {
                    throw new ArgumentException("El correo electrónico ingresado no es válido. Por favor, ingrese un correo electrónico correcto.");
                }

                // Validar la URL de la imagen
                if (!string.IsNullOrEmpty(model.ImageUrl) && !model.ImageUrl.IsValidUrl())
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

        // Funcion para traer todos los usuarios
        public async Task<List<UserDto>> GetUserDtos()
        {
            try
            {
                var users = await userRepository.GetAllAsync();
                return users.Select(user => mapper.Map<UserDto>(user)).ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener los usuarios: " + ex.Message, ex);
            }
        }

        // Obtener el nombre de usuario (Login)
        public async Task<List<RegisterUserDto>> GetLoginName(string name)
        {
            try
            {
                var users = await userRepository.FindAsync(u => u.Login.Contains(name));
                return users.Select(user => mapper.Map<RegisterUserDto>(user)).ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener los usuarios: " + ex.Message, ex);
            }
        }

        public async Task<LoginResponseDto> LoginDtos(LoginDto model)
        {
            try
            {
                // Buscar el usuario con el login y la contraseña encriptada
                var users = await userRepository.FindAsync(u => u.Login == model.Login && u.Password == model.Password.EncryptPassword());
                var user = users.FirstOrDefault();

                if (user == null)
                {
                    throw new ArgumentException("Credenciales inválidas.");
                }

                // Generar el token JWT
                var token = tokenService.GenerateToken(user);

                // Mapear los datos del usuario y el token al LoginResponseDto
                var responseDto = new LoginResponseDto
                {
                    Login = user.Login,
                    Token = token
                };

                return responseDto; // Devuelves el DTO con los datos y el token
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error en el proceso de inicio de sesión: " + ex.Message, ex);
            }
        }

        // Eliminar usuario por ID
        public async Task<bool> DeleteUser(long userId)
        {
            try
            {
                var userToDelete = await userRepository.GetByIdAsync(userId);

                if (userToDelete == null)
                {
                    throw new ApplicationException("El usuario no existe.");
                }

                await userRepository.DeleteAsync(userToDelete);
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al eliminar el usuario: " + ex.Message, ex);
            }
        }

        // Actualizar usuario
        public async Task<UserDto> UpdateUser(UserDto model)
        {
            try
            {
                var existingUser = await userRepository.GetByIdAsync(model.IdUser);

                if (existingUser == null)
                {
                    throw new ApplicationException("El usuario no existe.");
                }

                existingUser = mapper.Map(model, existingUser);
                var updatedUser = await userRepository.UpdateAsync(existingUser);

                return mapper.Map<UserDto>(updatedUser);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al actualizar el usuario: " + ex.Message, ex);
            }
        }
    }
}
