using backend_ecommerce.Response;
using ecommerce.BLL.Servicios;
using ecommerce.BLL.Servicios.Contrato;
using ecommerce.DTO.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTypeController : ControllerBase
    {
        private readonly IDocumentTypeService documentTypeService;

        public DocumentTypeController(IDocumentTypeService documentTypeService)
        {
            this.documentTypeService = documentTypeService;
        }

        [HttpGet("GetAllDocumentTypes")]
        public async Task<IActionResult> GetAllDocumentTypes()
        {
            var respuesta = new Response<List<DocumentTypeDto>>();

            try
            {
                // Obtener todos los tipos de documento
                var documentTypes = await documentTypeService.GetDocumentTypeAsync();

                // Si no hay tipos de documento, retorna un 404
                if (documentTypes == null || !documentTypes.Any())
                {
                    respuesta.Status = false;
                    respuesta.Message = "No se encontraron tipos de documento";
                    return NotFound(respuesta);  // Retorna 404 NotFound
                }

                // Si los tipos de documento fueron encontrados
                respuesta.Status = true;
                respuesta.Data = documentTypes.ToList();  // Asigna los tipos de documento a la respuesta
                respuesta.Message = "Tipos de documento obtenidos exitosamente";

                return Ok(respuesta);  // Retorna 200 OK con la lista de tipos de documento
            }
            catch (ApplicationException ex)
            {
                // Maneja excepciones de aplicación
                respuesta.Status = false;
                respuesta.Message = ex.Message;
                return BadRequest(respuesta);  // Retorna 400 BadRequest
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción inesperada
                respuesta.Status = false;
                respuesta.Message = "Se produjo un error inesperado: " + ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, respuesta);  // Retorna 500 Internal Server Error
            }
        }

        // Obtener un tipo de documento por nombre
        [HttpGet("GetDocumentTypeByName/{name}")]
        public async Task<IActionResult> GetDocumentTypeByName(string name)
        {
            var respuesta = new Response<DocumentTypeDto>(); 

            try
            {
                var documentType = await documentTypeService.GetDocumentTypeName(name);

                if (documentType == null)
                {
                    respuesta.Status = false;
                    respuesta.Message = "No se encontró el tipo de documento con el nombre proporcionado.";
                    return NotFound(respuesta); // 404 Not Found
                }

                respuesta.Status = true;
                respuesta.Data = documentType; // Asigna el tipo de documento a la propiedad Data
                respuesta.Message = "Tipo de documento obtenido exitosamente";

                return Ok(respuesta); // 200 OK con el tipo de documento
            }
            catch (ApplicationException ex)
            {
                // Maneja excepciones específicas lanzadas por el servicio
                respuesta.Status = false;
                respuesta.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, respuesta); // 500 Internal Server Error
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción que no sea de tipo ApplicationException
                respuesta.Status = false;
                respuesta.Message = "Se produjo un error inesperado: " + ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, respuesta); // 500 Internal Server Error
            }
        }
    }
}
