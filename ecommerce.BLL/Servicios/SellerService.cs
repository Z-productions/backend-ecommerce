using AutoMapper;
using ecommerce.BLL.ExtensionMetodos;
using ecommerce.BLL.Servicios.Contrato;
using ecommerce.DAL.Repository.Contrato;
using ecommerce.DTO.Common;
using ecommerce.DTO.Registration;
using ecommerce.MODEL;

namespace ecommerce.BLL.Servicios
{
    public class SellerService : ISellerService
    {
        private readonly IGenericRepository<Seller> sellerRepository;
        private readonly IGenericRepository<DocumentType> documentTypeRepository;
        private readonly IGenericRepository<UserDto> userRepository;
        private readonly IMapper mapper;

        public SellerService(IGenericRepository<Seller> sellerRepository, IGenericRepository<DocumentType> documentTypeRepository, IGenericRepository<UserDto> userRepository, IMapper mapper)
        {
            this.sellerRepository = sellerRepository;
            this.documentTypeRepository = documentTypeRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<RegisterSellerDto> CreateSeller(RegisterSellerDto model)
        {
            try
            {
                // Validar el DTO usando el método de extensión (campos vacíos)
                model.ValidateDto();

                // Validar que el número de documento contenga solo números
                if (!model.DocumentNumber.IsNumeric())
                {
                    throw new ArgumentException("El número de documento debe contener únicamente dígitos.");
                }

                // Validar que el campo del banco no esté vacío
                if (string.IsNullOrWhiteSpace(model.Bank))
                {
                    throw new ArgumentException("El campo del banco no puede estar vacío.");
                }

                // Validar que el número de cuenta contenga solo números
                if (!model.AccountNumber.IsNumeric())
                {
                    throw new ArgumentException("El número de cuenta debe contener únicamente dígitos.");
                }

                // Validar la longitud del número de cuenta
                if (model.AccountNumber.Length < 8 || model.AccountNumber.Length > 15)
                {
                    throw new ArgumentException("El número de cuenta debe tener entre 8 y 15 dígitos.");
                }

                // Validar si el comprador con este número de documento ya existe (evitar duplicados)
                var existingBuyer = await sellerRepository.FindAsync(document => model.DocumentNumber == model.DocumentNumber);

                if (existingBuyer.Any())
                {
                    throw new ArgumentException("Ya existe un comprador registrado con este número de documento.");
                }

                // Mapear el DTO al modelo y agregarlo
                var seller = mapper.Map<Seller>(model);
                var sellerCreate = await sellerRepository.AddAsync(seller);


                // Retornar el DTO del comprador creado
                return mapper.Map<RegisterSellerDto>(sellerCreate);
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

        public async Task<bool> DeleteSeller(long sellerId)
        {
            try
            {
                // Buscar el vendedor por ID
                var sellerToDelete = await sellerRepository.GetByIdAsync(sellerId);

                // Si el vendedor no existe, lanzar una excepción
                if (sellerToDelete == null)
                {
                    throw new ArgumentException("No se encontró ningún vendedor con el ID proporcionado.");
                }

                // Eliminar el vendedor de la base de datos
                await sellerRepository.DeleteAsync(sellerToDelete);

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
                throw new ApplicationException($"Ocurrió un error inesperado al intentar eliminar el vendedor. Por favor, intente de nuevo más tarde. {ex.Message}");
            }
        }

        public async Task<bool> UpdateSeller(SellerDto model)
        {
            try
            {
                // Buscar el vendedor por ID
                var existingSeller = await sellerRepository.GetByIdAsync(model.Id);
                
                if (existingSeller == null)
                {
                    throw new ApplicationException("El vendedor no existe.");
                }

                // Verificar y actualizar el banco si ha cambiado
                if (!string.Equals(existingSeller.Bank, model.Bank, StringComparison.OrdinalIgnoreCase))
                {
                    if (string.IsNullOrWhiteSpace(model.Bank))
                    {
                        throw new ArgumentException("El campo Banco no puede estar vacío.");
                    }
                    existingSeller.Bank = model.Bank;
                }

                // Verificar y actualizar el número de cuenta si ha cambiado
                if (!string.Equals(existingSeller.AccountNumber, model.AccountNumber, StringComparison.OrdinalIgnoreCase))
                {
                    if (!model.AccountNumber.IsNumeric())
                    {
                        throw new ArgumentException("El número de cuenta solo debe contener dígitos.");
                    }
                    existingSeller.AccountNumber = model.AccountNumber;
                }


                // Verificar y actualizar el número de documento si ha cambiado
                if (!string.Equals(existingSeller.DocumentNumber, model.DocumentNumber, StringComparison.OrdinalIgnoreCase))
                {
                    if (!model.DocumentNumber.IsNumeric())
                    {
                        throw new ArgumentException("El número de documento solo debe contener dígitos.");
                    }

                    // Verificar si el número de documento ya está en uso por otro vendedor
                    var existingSellerWithSameDoc = await sellerRepository.FindAsync(s => s.DocumentNumber == model.DocumentNumber && s.Id != model.Id);
                    if (existingSellerWithSameDoc.Any())
                    {
                        throw new ArgumentException("Ya existe un vendedor con este número de documento.");
                    }
                    existingSeller.DocumentNumber = model.DocumentNumber;
                }


                // Verificar y actualizar el tipo de documento si ha cambiado
                if (existingSeller.DocumentTypeId != model.DocumentTypeId)
                {
                    var documentType = await documentTypeRepository.GetByIdAsync(model.DocumentTypeId);
                    if (documentType == null)
                    {
                        throw new ArgumentException("El tipo de documento especificado no existe.");
                    }
                    existingSeller.DocumentTypeId = model.DocumentTypeId;
                }

                // Verificar y actualizar el ID de usuario si ha cambiado
                if (existingSeller.UserId != model.UserId)
                {
                    var user = await userRepository.GetByIdAsync(model.UserId);
                    if (user == null)
                    {
                        throw new ArgumentException("El usuario especificado no existe.");
                    }
                    existingSeller.UserId = model.UserId;
                }

                // Actualizar el vendedor en la base de datos
                var updateResult = await sellerRepository.UpdateAsync(existingSeller);
                return updateResult.HasValue && updateResult.Value;
            }
            catch (ArgumentException ex)
            {
                throw new ApplicationException($"Error en los datos proporcionados: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Ocurrió un error inesperado al actualizar el vendedor. Por favor, intente de nuevo más tarde. {ex.Message}");
            }
        }
    }
}
