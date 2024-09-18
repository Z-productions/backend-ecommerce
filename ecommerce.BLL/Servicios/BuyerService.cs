using AutoMapper;
using ecommerce.BLL.ExtensionMetodos;
using ecommerce.BLL.Servicios.Contrato;
using ecommerce.DAL.Repository.Contrato;
using ecommerce.DTO.Common;
using ecommerce.DTO.Registration;
using ecommerce.MODEL;

namespace ecommerce.BLL.Servicios
{
    public class BuyerService : IBuyerService
    {
        private readonly IGenericRepository<Buyer> buyerRepository;
        private readonly IGenericRepository<DocumentType> documentTypeRepository;
        private readonly IMapper mapper;

        public BuyerService(IGenericRepository<Buyer> buyerRepository, IGenericRepository<DocumentType> documentTypeRepository, IMapper mapper)
        {
            this.buyerRepository = buyerRepository;
            this.documentTypeRepository = documentTypeRepository;
            this.mapper = mapper;
        }



        // Crear comprador
        public async Task<RegisterBuyerDto> CreateBuyer(RegisterBuyerDto model)
        {
            try
            {
                // Validar el DTO usando el método de extensión (campos vacíos)
                model.ValidateDto();

                // Validar que el número de teléfono contenga solo números
                if (!model.Phone.IsNumeric())
                {
                    throw new ArgumentException("El número de teléfono solo debe contener dígitos.");
                }

                // Validar que el número de documento contenga solo números
                if (!model.DocumentNumber.IsNumeric())
                {
                    throw new ArgumentException("El número de documento solo debe contener dígitos.");
                }

                // Validar que el número de documento contenga solo números
                if (!model.DocumentNumber.IsNumeric())
                {
                    throw new ArgumentException("El número de documento solo debe contener dígitos.");
                }

                // Validar longitud del número de documento
                if (model.DocumentNumber.Length < 8 || model.DocumentNumber.Length > 15)
                {
                    throw new ArgumentException("El número de documento debe tener entre 8 y 15 dígitos.");
                }

                // Validar que el campo de dirección no esté vacío
                if (string.IsNullOrWhiteSpace(model.Address))
                {
                    throw new ArgumentException("La dirección no puede estar vacía.");
                }

                // Validar si el comprador con este número de documento ya existe (evitar duplicados)
                var existingBuyer = await buyerRepository.FindAsync(document => model.DocumentNumber == model.DocumentNumber);
               
                if (existingBuyer.Any())
                {
                    throw new ArgumentException("Ya existe un comprador registrado con este número de documento.");
                }

                // Mapear el DTO al modelo y agregarlo
                var buyer = mapper.Map<Buyer>(model);  
                var buyerCreate = await buyerRepository.AddAsync(buyer);


                // Retornar el DTO del comprador creado
                return mapper.Map<RegisterBuyerDto>(buyerCreate);
            }
            catch (TaskCanceledException ex)
            {
                // Mensaje específico para errores de cancelación de tareas
                throw new ApplicationException($"El proceso fue cancelado. Inténtalo nuevamente más tarde. {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                // Mensaje específico para errores de validación
                throw new ApplicationException($"Error en los datos ingresados: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Mensaje genérico para cualquier otro tipo de excepción
                throw new ApplicationException($"Ocurrió un error inesperado. Por favor, inténtalo nuevamente más tarde. {ex.Message}");
            }
        }

        // Borrar Comprador
        public async Task<bool> DeleteBuyer(long buyerId)
        {
            try
            {
                // Buscar el comprador por ID
                var buyerToDelete = await buyerRepository.GetByIdAsync(buyerId);

                // Si el comprador no existe, lanzar una excepción
                if (buyerToDelete == null)
                {
                    throw new ArgumentException("No se encontró ningún comprador con el ID proporcionado.");
                }

                // Eliminar el comprador de la base de datos
                await buyerRepository.DeleteAsync(buyerToDelete);

                // Devolver true si la operación fue exitosa
                return true;
            }
            catch (ArgumentException ex)
            {
                // Mensaje específico para errores de validación
                throw new ApplicationException($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Mensaje genérico para cualquier otro tipo de excepción
                throw new ApplicationException($"Ocurrió un error inesperado al intentar eliminar el comprador. Por favor, intente de nuevo más tarde. {ex.Message}");
            }
        }

        // Actualizar comprador
        public async Task<bool> UpdateBuyer(BuyerDto model)
        {
            try
            {
                // Buscar el comprador por ID
                var existingBuyer = await buyerRepository.GetByIdAsync(model.Id);

                if (existingBuyer == null)
                {
                    throw new ApplicationException("El comprador no existe.");
                }

                // Verificar si el número de teléfono ha cambiado
                if (!string.Equals(existingBuyer.Phone, model.Phone, StringComparison.OrdinalIgnoreCase))
                {
                    if (!model.Phone.IsNumeric())
                    {
                        throw new ArgumentException("El número de teléfono solo debe contener dígitos.");
                    }
                    existingBuyer.Phone = model.Phone;
                }

                // Verificar si la dirección ha cambiado
                if (!string.Equals(existingBuyer.Address, model.Address, StringComparison.OrdinalIgnoreCase))
                {
                    if (string.IsNullOrWhiteSpace(model.Address))
                    {
                        throw new ArgumentException("La dirección no puede estar vacía.");
                    }
                    existingBuyer.Address = model.Address;
                }

                // Verificar si el número de documento ha cambiado
                if (!string.Equals(existingBuyer.DocumentNumber, model.DocumentNumber, StringComparison.OrdinalIgnoreCase))
                {
                    if (!model.DocumentNumber.IsNumeric())
                    {
                        throw new ArgumentException("El número de documento solo debe contener dígitos.");
                    }

                    // Verificar si el número de documento ya está en uso por otro comprador
                    var existingBuyerWithSameDoc = await buyerRepository.FindAsync(b => b.DocumentNumber == model.DocumentNumber && b.Id != model.Id);
                    if (existingBuyerWithSameDoc.Any())
                    {
                        throw new ArgumentException("Ya existe un comprador con este número de documento.");
                    }

                    existingBuyer.DocumentNumber = model.DocumentNumber;
                }

                // Verificar si el tipo de documento ha cambiado
                if (existingBuyer.DocumentTypeId != model.DocumentTypeId)
                {
                    // Asegúrate de que el tipo de documento existe
                    var documentType = await documentTypeRepository.GetByIdAsync(model.DocumentTypeId);
                    if (documentType == null)
                    {
                        throw new ArgumentException("El tipo de documento especificado no existe.");
                    }

                    existingBuyer.DocumentTypeId = model.DocumentTypeId;
                }

                // Verificar si el usuario ha cambiado (si el modelo de Buyer está relacionado con el modelo de User)
                if (existingBuyer.UserId != model.UserId)
                {
                    // Asegúrate de que el usuario existe
                    var user = await buyerRepository.GetByIdAsync(model.UserId);
                    if (user == null)
                    {
                        throw new ArgumentException("El usuario especificado no existe.");
                    }

                    existingBuyer.UserId = model.UserId;
                }

                // Actualizar el comprador en la base de datos
                var updateResult = await buyerRepository.UpdateAsync(existingBuyer);

                return updateResult.HasValue && updateResult.Value;
            }
            catch (ArgumentException ex)
            {
                // Mensaje específico para errores de validación
                throw new ApplicationException($"Error en los datos proporcionados: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Mensaje genérico para cualquier otro tipo de excepción
                throw new ApplicationException($"Ocurrió un error inesperado al actualizar el comprador. Por favor, intente de nuevo más tarde. {ex.Message}");
            }
        }
    }
}
