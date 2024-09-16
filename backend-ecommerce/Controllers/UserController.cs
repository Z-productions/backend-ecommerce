using ecommerce.DTO.Registration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using backend_ecommerce.Response;
using ecommerce.BLL.Servicios.Contrato;
using Microsoft.AspNetCore.Authorization;
using ecommerce.DTO.Login;
using ecommerce.DTO.Common;


namespace backend_ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices userService;


        public UserController(IUserServices userService)
        {
            this.userService = userService;
        }

        [HttpPost("create/user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            var respuesta = new Response<List<RegisterUserDto>>();

            try
            {
                // Validar si el DTO es válido 
                if (registerUserDto == null)
                {
                    respuesta.Status = false;
                    respuesta.Message = "Los datos proporcionados no son válidos. Por favor, revise la información e intente nuevamente.";
                    return BadRequest(respuesta);  // Retorna 400 BadRequest
                }

                var usuario = await userService.CreateUser(registerUserDto);

                // Si el usuario no es encontrado o no pudo ser creado, puedes retornar un 404 NotFound
                if (usuario == null)
                {
                    respuesta.Status = false;
                    respuesta.Message = "No se pudo crear el usuario. Inténtelo nuevamente.";
                    return NotFound(respuesta);  // Retorna 404 NotFound
                }

                // Si el usuario fue creado correctamente
                respuesta.Status = true;
                respuesta.Data = new List<RegisterUserDto> { usuario }; // Encapsulamos el usuario en una lista
                respuesta.Message = "Usuario creado exitosamente";

                return Ok(respuesta);  // Retornar respuesta con estado 200 OK
            }
            catch (ArgumentException ex)
            {
                // Maneja errores específicos lanzados por el servicio
                respuesta.Status = false;
                respuesta.Message = ex.Message;
                return BadRequest(respuesta);  // Retorna 400 BadRequest
            }
            catch (ApplicationException ex)
            {
                // Maneja excepciones genéricas lanzadas por el servicio
                respuesta.Status = false;
                respuesta.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, respuesta);  // Retorna 500 Internal Server Error
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción que no sea de tipo ApplicationException
                respuesta.Status = false;
                respuesta.Message = "Se produjo un error inesperado: " + ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, respuesta);  // Retorna 500 Internal Server Error
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var respuesta = new Response<LoginResponseDto>(); // Aquí usa LoginResponseDto, no LoginDto
            try
            {
                // Llama al servicio para autenticar al usuario y obtener el token
                var loginResult = await userService.LoginDtos(model);

                // Verifica si el resultado es nulo o no se ha generado el token
                if (loginResult == null || string.IsNullOrEmpty(loginResult.Token))
                {
                    respuesta.Status = false;
                    respuesta.Message = "Credenciales inválidas.";
                    return BadRequest(respuesta);  // Retorna 400 BadRequest si no es exitoso
                }

                // Si las credenciales son válidas, devuelve el login y el token
                respuesta.Status = true;
                respuesta.Data = loginResult; // Asegúrate de que loginResult sea de tipo LoginResponseDto
                respuesta.Message = "Login exitoso";

                return Ok(respuesta);  // Retorna 200 OK con el token y los datos del usuario
            }
            catch (ArgumentException ex)
            {
                // Maneja los errores de credenciales inválidas
                respuesta.Status = false;
                respuesta.Message = ex.Message;
                return BadRequest(respuesta);  // Retorna 400 BadRequest si hay error de credenciales
            }
            catch (ApplicationException ex)
            {
                // Maneja errores en el servicio
                respuesta.Status = false;
                respuesta.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, respuesta);  // Retorna 500 si hay un error interno
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción inesperada
                respuesta.Status = false;
                respuesta.Message = "Se produjo un error inesperado: " + ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, respuesta);  // Retorna 500 por error inesperado
            }
        }

        [HttpGet("get/user/{name}")]
        public async Task<IActionResult> GetLoginName(string name)
        {
            var respuesta = new Response<List<RegisterUserDto>>();

            try
            {
                // Llama al servicio para obtener los usuarios cuyo Login contiene el nombre proporcionado
                var usuarios = await userService.GetLoginName(name);

                // Si no se encuentran usuarios, devuelve un mensaje adecuado
                if (usuarios == null || !usuarios.Any())
                {
                    respuesta.Status = false;
                    respuesta.Message = "No se encontraron usuarios con el nombre proporcionado.";
                    return NotFound(respuesta);  // Retorna 404 NotFound si no hay resultados
                }

                // Si se encuentran usuarios, retorna la lista
                respuesta.Status = true;
                respuesta.Data = usuarios;
                respuesta.Message = "Usuarios encontrados con éxito";

                return Ok(respuesta);  // Retorna 200 OK con los usuarios encontrados
            }
            catch (ArgumentException ex)
            {
                // Maneja los errores de argumentos inválidos
                respuesta.Status = false;
                respuesta.Message = ex.Message;
                return BadRequest(respuesta);  // Retorna 400 BadRequest
            }
            catch (ApplicationException ex)
            {
                // Maneja errores de aplicación
                respuesta.Status = false;
                respuesta.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, respuesta);  // Retorna 500 si hay un error interno
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción inesperada
                respuesta.Status = false;
                respuesta.Message = "Se produjo un error inesperado: " + ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, respuesta);  // Retorna 500 por error inesperado
            }
        }
        
        [Authorize]
        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteUser(long userId)
        {
            var respuesta = new Response<bool>();

            try
            {
                // Elimina el usuario usando el servicio
                var result = await userService.DeleteUser(userId);

                respuesta.Status = result;
                respuesta.Message = result ? "Usuario eliminado exitosamente" : "No se pudo eliminar el usuario";

                return result ? Ok(respuesta) : NotFound(respuesta);
            }
            catch (ApplicationException ex)
            {
                respuesta.Status = false;
                respuesta.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Status = false;
                respuesta.Message = "Se produjo un error inesperado: " + ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, respuesta);
            }
        }

        [HttpGet("get/users")]
        public async Task<IActionResult> GetUsers()
        {
            var respuesta = new Response<List<UserDto>>();

            try
            {
                var users = await userService.GetUserDtos();

                respuesta.Status = true;
                respuesta.Data = users;
                respuesta.Message = "Usuarios obtenidos exitosamente";

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Status = false;
                respuesta.Message = "Error al obtener los usuarios: " + ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, respuesta);
            }
        }

        [Authorize]
        [HttpPut("update/user")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto userDto)
        {
            var respuesta = new Response<UserDto>();

            try
            {
                if (userDto == null)
                {
                    respuesta.Status = false;
                    respuesta.Message = "Los datos proporcionados no son válidos.";
                    return BadRequest(respuesta);
                }

                var updatedUser = await userService.UpdateUser(userDto);

                if (updatedUser == null)
                {
                    respuesta.Status = false;
                    respuesta.Message = "No se pudo actualizar el usuario.";
                    return NotFound(respuesta);
                }

                respuesta.Status = true;
                respuesta.Message = "Usuario actualizado exitosamente";

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Status = false;
                respuesta.Message = "Error al actualizar el usuario: " + ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, respuesta);
            }
        }
    }
}
