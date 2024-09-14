using ecommerce.DTO.Registration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using backend_ecommerce.Response;
using ecommerce.BLL.Servicios.Contrato;


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

        [HttpPost]
        [Route("create/user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            var respuesta = new Response<List<RegisterUserDto>>();

            try
            {
                // Validar si el DTO es válido 
                if (registerUserDto == null)
                {
                    respuesta.Status = false;
                    respuesta.Message = "Datos de usuario no válidos";
                    return BadRequest(respuesta);  // Retorna 400 BadRequest
                }

                var usuario = await userService.CreateUser(registerUserDto);

                // Si el usuario no es encontrado o no pudo ser creado, puedes retornar un 404 NotFound
                if (usuario == null)
                {
                    respuesta.Status = false;
                    respuesta.Message = "Usuario no encontrado";
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
    }
}
