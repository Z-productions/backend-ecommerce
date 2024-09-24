using backend_ecommerce.Response;
using ecommerce.BLL.Servicios;
using ecommerce.BLL.Servicios.Contrato;
using ecommerce.DTO.Common;
using ecommerce.DTO.Registration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyerController : ControllerBase
    {
        private readonly IBuyerService buyerService;

        public BuyerController(IBuyerService buyerService)
        {
            this.buyerService = buyerService;
        }

        // POST: api/Buyer/create
        [HttpPost("create")]
        public async Task<IActionResult> RegisterBuyer([FromBody] RegisterBuyerDto registerBuyerDto)
        {
            var respuesta = new Response<RegisterBuyerDto>();

            try
            {
                // Validar si el DTO es válido
                if (registerBuyerDto == null)
                {
                    respuesta.Status = false;
                    respuesta.Message = "Los datos proporcionados no son válidos. Por favor, revise la información e intente nuevamente.";
                    return BadRequest(respuesta); // Retorna 400 BadRequest
                }

                var buyer = await buyerService.CreateBuyer(registerBuyerDto);

                // Si el comprador no pudo ser creado, retorna un 404 NotFound
                if (buyer == null)
                {
                    respuesta.Status = false;
                    respuesta.Message = "No se pudo crear el comprador. Inténtelo nuevamente.";
                    return NotFound(respuesta); // Retorna 404 NotFound
                }

                // Si el comprador fue creado correctamente
                respuesta.Status = true;
                respuesta.Data = buyer;
                respuesta.Message = "Comprador creado exitosamente";

                return Ok(respuesta); // Retornar respuesta con estado 200 OK
            }
            catch (ArgumentException ex)
            {
                // Maneja errores específicos lanzados por el servicio
                respuesta.Status = false;
                respuesta.Message = ex.Message;
                return BadRequest(respuesta); // Retorna 400 BadRequest
            }
            catch (ApplicationException ex)
            {
                // Maneja excepciones genéricas lanzadas por el servicio
                respuesta.Status = false;
                respuesta.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, respuesta); // Retorna 500 Internal Server Error
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción que no sea de tipo ApplicationException
                respuesta.Status = false;
                respuesta.Message = "Se produjo un error inesperado: " + ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, respuesta); // Retorna 500 Internal Server Error
            }
        }

        // GET: api/Buyer/getByDocumentNumber/{documentNumber}
        [HttpGet("getByDocumentNumber/{documentNumber}"), Authorize]
        public async Task<IActionResult> GetBuyerByDocumentNumber(string documentNumber)
        {
            var respuesta = new Response<BuyerWithUserDto>();

            try
            {
                // Validar que el número de documento no sea nulo o vacío
                if (string.IsNullOrWhiteSpace(documentNumber))
                {
                    respuesta.Status = false;
                    respuesta.Message = "El número de documento proporcionado no es válido.";
                    return BadRequest(respuesta); // Retorna 400 BadRequest
                }

                var buyerWithUser = await buyerService.GetBuyerWithUserInfoByDocumentNumber(documentNumber);


                // Si no se encontró el comprador, retorna un 404 NotFound
                if (buyerWithUser == null)
                {
                    respuesta.Status = false;
                    respuesta.Message = "No se encontró ningún comprador con ese número de documento.";
                    return NotFound(respuesta); // Retorna 404 NotFound
                }

                // Si se encontró el comprador correctamente
                respuesta.Status = true;
                respuesta.Data = buyerWithUser;
                respuesta.Message = "Comprador encontrado exitosamente";

                return Ok(respuesta); // Retornar respuesta con estado 200 OK
            }
            catch (ArgumentException ex)
            {
                // Maneja errores específicos lanzados por el servicio
                respuesta.Status = false;
                respuesta.Message = ex.Message;
                return BadRequest(respuesta); // Retorna 400 BadRequest
            }
            catch (ApplicationException ex)
            {
                // Maneja excepciones genéricas lanzadas por el servicio
                respuesta.Status = false;
                respuesta.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, respuesta); // Retorna 500 Internal Server Error
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción que no sea de tipo ApplicationException
                respuesta.Status = false;
                respuesta.Message = "Se produjo un error inesperado: " + ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, respuesta); // Retorna 500 Internal Server Error
            }
        }

        // PUT: api/Buyer/update
        [HttpPut("update"), Authorize]
        public async Task<IActionResult> UpdateBuyer([FromBody] BuyerDto updateBuyerDto)
        {
            var respuesta = new Response<BuyerDto>();

            try
            {
                // Validar si el DTO es válido
                if (updateBuyerDto == null)
                {
                    respuesta.Status = false;
                    respuesta.Message = "Los datos proporcionados no son válidos. Por favor, revise la información e intente nuevamente.";
                    return BadRequest(respuesta); // Retorna 400 BadRequest
                }

                var buyer = await buyerService.UpdateBuyer(updateBuyerDto);

                // Si el comprador no pudo ser actualizado, retorna un 404 NotFound
                if (!buyer)
                {
                    respuesta.Status = false;
                    respuesta.Message = "No se pudo actualizar el comprador. Inténtelo nuevamente.";
                    return NotFound(respuesta); // Retorna 404 NotFound
                }

                // Si el comprador fue actualizado correctamente
                respuesta.Status = true;
                respuesta.Message = "Comprador actualizado exitosamente";

                return Ok(respuesta); // Retornar respuesta con estado 200 OK
            }
            catch (ArgumentException ex)
            {
                // Maneja errores específicos lanzados por el servicio
                respuesta.Status = false;
                respuesta.Message = ex.Message;
                return BadRequest(respuesta); // Retorna 400 BadRequest
            }
            catch (ApplicationException ex)
            {
                // Maneja excepciones genéricas lanzadas por el servicio
                respuesta.Status = false;
                respuesta.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, respuesta); // Retorna 500 Internal Server Error
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción que no sea de tipo ApplicationException
                respuesta.Status = false;
                respuesta.Message = "Se produjo un error inesperado: " + ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, respuesta); // Retorna 500 Internal Server Error
            }
        }

        // DELETE: api/Buyer/delete/5
        [HttpDelete("delete/{id}"), Authorize]
        public async Task<IActionResult> DeleteBuyer(long id)
        {
            var respuesta = new Response<bool>();

            try
            {
                // Elimina el comprador usando el servicio
                var result = await buyerService.DeleteBuyer(id);

                respuesta.Status = result;
                respuesta.Message = result ? "Comprador eliminado exitosamente" : "No se pudo eliminar el comprador";

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
    }
}
