using AutoMapper;
using ecommerce.BLL.Servicios.Contrato;
using ecommerce.DAL.Repository.Contrato;
using ecommerce.DTO.Common;
using ecommerce.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ecommerce.BLL.Servicios
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly IGenericRepository<DocumentType> documentRepository;
        private readonly IMapper mapper;

        public DocumentTypeService(IGenericRepository<DocumentType> documentRepository, IMapper mapper)
        {
            this.documentRepository = documentRepository;
            this.mapper = mapper;
        }

        public async Task<List<DocumentTypeDto>> GetDocumentTypeAsync()
        {
            try
            {
                // Mapear el DTO al modelo de usuario y agregarlo
                var documentTypes = await documentRepository.GetAllAsync();

                return mapper.Map<List<DocumentTypeDto>>(documentTypes);

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

        public async Task<DocumentTypeDto> GetDocumentTypeName(string name)
        {
            try
            {
                // Mapear el DTO al modelo de usuario y agregarlo
                var getDocument = await documentRepository.FindAsync(dt => dt.DocumentName == name);

                return mapper.Map<DocumentTypeDto>(getDocument.First());

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
