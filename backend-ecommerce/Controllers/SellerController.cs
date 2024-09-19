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
    public class SellerController : ControllerBase
    {
        private readonly ISellerService sellerService;
        public SellerController(ISellerService sellerService)
        {
            this.sellerService = sellerService;
        }

        // POST: api/Seller/create
        [HttpPost("create")]
        public async Task<IActionResult> RegisterSeller([FromBody] RegisterSellerDto registerSellerDto)
        {
            var respuesta = new Response<RegisterSellerDto>();

            try
            {
                // Validar si el DTO es válido
                if (registerSellerDto == null)
                {
                    respuesta.Status = false;
                    respuesta.Message = "Los datos proporcionados no son válidos. Por favor, revise la información e intente nuevamente.";
                    return BadRequest(respuesta); // Retorna 400 BadRequest
                }

                var buyer = await sellerService.CreateSeller(registerSellerDto);

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

        // PUT: api/Seller/update
        [HttpPut("update"), Authorize]
        public async Task<IActionResult> UpdateSeller([FromBody] SellerDto updateSellerDto)
        {
            var respuesta = new Response<BuyerDto>();

            try
            {
                // Validar si el DTO es válido
                if (updateSellerDto == null)
                {
                    respuesta.Status = false;
                    respuesta.Message = "Los datos proporcionados no son válidos. Por favor, revise la información e intente nuevamente.";
                    return BadRequest(respuesta); // Retorna 400 BadRequest
                }

                var buyer = await sellerService.UpdateSeller(updateSellerDto);

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

        // DELETE: api/Seller/delete/5
        [HttpDelete("delete/{id}"), Authorize]
        public async Task<IActionResult> DeleteSeller(long id)
        {
            var respuesta = new Response<bool>();

            try
            {
                // Elimina el comprador usando el servicio
                var result = await sellerService.DeleteSeller(id);

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
