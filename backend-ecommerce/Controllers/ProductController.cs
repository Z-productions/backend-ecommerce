using backend_ecommerce.Response;
using ecommerce.BLL.Servicios;
using ecommerce.BLL.Servicios.Contrato;
using ecommerce.DTO.Common;
using ecommerce.DTO.Registration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        // POST: api/Product/create
        [HttpPost("create")]
        public async Task<IActionResult> RegisterProdcut([FromBody] RegisterProduct registerProduct)
        {
            var respuesta = new Response<RegisterProduct>();

            try
            {
                // Validar si el DTO es válido
                if (registerProduct == null)
                {
                    respuesta.Status = false;
                    respuesta.Message = "Los datos proporcionados no son válidos. Por favor, revise la información e intente nuevamente.";
                    return BadRequest(respuesta); // Retorna 400 BadRequest
                }

                var buyer = await productService.CreateProduct(registerProduct);

                // Si el comprador no pudo ser creado, retorna un 404 NotFound
                if (buyer == null)
                {
                    respuesta.Status = false;
                    respuesta.Message = "No se pudo crear el producto. Inténtelo nuevamente.";
                    return NotFound(respuesta); // Retorna 404 NotFound
                }

                // Si el comprador fue creado correctamente
                respuesta.Status = true;
                respuesta.Data = buyer;
                respuesta.Message = "Producto creado exitosamente";

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

        // GET: api/product
        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            var respuesta = new Response<List<ProductDto>>();

            try
            {
                var users = await productService.GetProduct();

                respuesta.Status = true;
                respuesta.Data = users;
                respuesta.Message = "Productos obtenidos exitosamente";

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Status = false;
                respuesta.Message = "Error al obtener los productos: " + ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, respuesta);
            }
        }

        // PUT: api/Product/update
        [HttpPut("update"), Authorize]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto updateProductDto)
        {
            var respuesta = new Response<BuyerDto>();

            try
            {
                // Validar si el DTO es válido
                if (updateProductDto == null)
                {
                    respuesta.Status = false;
                    respuesta.Message = "Los datos proporcionados no son válidos. Por favor, revise la información e intente nuevamente.";
                    return BadRequest(respuesta); // Retorna 400 BadRequest
                }

                var buyer = await productService.UpdateProduct(updateProductDto);

                // Si el comprador no pudo ser actualizado, retorna un 404 NotFound
                if (!buyer)
                {
                    respuesta.Status = false;
                    respuesta.Message = "No se pudo actualizar el producto. Inténtelo nuevamente.";
                    return NotFound(respuesta); // Retorna 404 NotFound
                }

                // Si el producto fue actualizado correctamente
                respuesta.Status = true;
                respuesta.Message = "´Producto actualizado exitosamente";

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

        // DELETE: api/Product/delete/5
        [HttpDelete("delete/{id}"), Authorize]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            var respuesta = new Response<bool>();

            try
            {
                // Elimina el comprador usando el servicio
                var result = await productService.DeleteProduct(id);

                respuesta.Status = result;
                respuesta.Message = result ? "Producto eliminado exitosamente" : "No se pudo eliminar el producto";

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
