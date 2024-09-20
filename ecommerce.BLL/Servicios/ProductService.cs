using AutoMapper;
using ecommerce.BLL.ExtensionMetodos;
using ecommerce.BLL.Servicios.Contrato;
using ecommerce.DAL.Repository.Contrato;
using ecommerce.DTO.Common;
using ecommerce.DTO.Registration;
using ecommerce.MODEL;

namespace ecommerce.BLL.Servicios
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> productRepository;
        private readonly IMapper mapper;

        public ProductService(IGenericRepository<Product> productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<RegisterProduct> CreateProduct(RegisterProduct model)
        {
            try
            {
                // Validar el DTO usando el método de extensión (campos vacíos)
                model.ValidateDto();

                // Validar que el número del precio contenga solo números
                if (!string.IsNullOrEmpty(model.Price) &&!model.Price.IsNumeric())
                {
                    throw new ArgumentException("El número del precio debe contener únicamente dígitos.");
                }

                // Validar que el número del precio contenga solo números
                if (!string.IsNullOrEmpty(model.Stock) && !model.Stock.IsNumeric())
                {
                    throw new ArgumentException("El número del stock debe contener únicamente dígitos.");
                }

                // Validar la URL de la imagen
                if (!string.IsNullOrEmpty(model.UrlImage) && !model.UrlImage.IsValidUrl())
                {
                    throw new ArgumentException("La URL de la imagen proporcionada no es válida. Por favor, verifique que sea una dirección URL correcta.");
                }

               
                // Mapear el DTO al modelo y agregarlo
                var product = mapper.Map<Product>(model);
                var productCreate = await productRepository.AddAsync(product);

                // Retornar el DTO del comprador creado
                return mapper.Map<RegisterProduct>(productCreate);
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

        public async Task<bool> DeleteProduct(long productId)
        {
            try
            {
                var productToDelete = await productRepository.GetByIdAsync(productId);

                if (productId == null)
                {
                    throw new ApplicationException("El producto no existe.");
                }

                await productRepository.DeleteAsync(productToDelete);
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al eliminar el usuario: " + ex.Message, ex);
            }
        }

        public async Task<bool> UpdateProduct(ProductDto model)
        {
            try
            {
                // Buscar el producto por ID
                var existingProduct = await productRepository.GetByIdAsync(model.Id);

                if (existingProduct == null)
                {
                    throw new ApplicationException("El producto no existe.");
                }

                // Verificar si el código del producto ha cambiado
                if (!string.Equals(existingProduct.Code, model.Code, StringComparison.OrdinalIgnoreCase))
                {
                    if (string.IsNullOrWhiteSpace(model.Code))
                    {
                        throw new ArgumentException("El código del producto no puede estar vacío.");
                    }
                    existingProduct.Code = model.Code;
                }


                // Verificar si el nombre del producto ha cambiado
                if (!string.Equals(existingProduct.Name, model.Name, StringComparison.OrdinalIgnoreCase))
                {
                    if (string.IsNullOrWhiteSpace(model.Name))
                    {
                        throw new ArgumentException("El nombre del producto no puede estar vacío.");
                    }
                    existingProduct.Name = model.Name;
                }

                // Verificar si la descripción ha cambiado
                if (!string.Equals(existingProduct.Description, model.Description, StringComparison.OrdinalIgnoreCase))
                {
                    if (string.IsNullOrWhiteSpace(model.Description))
                    {
                        throw new ArgumentException("La descripción no puede estar vacía.");
                    }
                    existingProduct.Description = model.Description;
                }

                // Verificar si la marca ha cambiado
                if (!string.Equals(existingProduct.Brand, model.Brand, StringComparison.OrdinalIgnoreCase))
                {
                    if (string.IsNullOrWhiteSpace(model.Brand))
                    {
                        throw new ArgumentException("La marca no puede estar vacía.");
                    }
                    existingProduct.Brand = model.Brand;
                }

                // Verificar si el precio ha cambiado
                if (existingProduct.Price != model.Price)
                {
                    if (model.Price <= 0)
                    {
                        throw new ArgumentException("El precio debe ser un número positivo.");
                    }
                    existingProduct.Price = model.Price;
                }

                // Verificar si el stock ha cambiado
                if (existingProduct.Stock != model.Stock)
                {
                    if (model.Stock < 0)
                    {
                        throw new ArgumentException("El stock no puede ser negativo.");
                    }
                    existingProduct.Stock += model.Stock;
                }

                // Verificar si la URL de la imagen ha cambiado
                if (!string.Equals(existingProduct.UrlImage, model.UrlImage, StringComparison.OrdinalIgnoreCase))
                {
                    if (string.IsNullOrWhiteSpace(model.UrlImage) && model.UrlImage.IsValidUrl())
                    {
                        throw new ArgumentException("La URL de la imagen no puede estar vacía.");
                    }
                    existingProduct.UrlImage = model.UrlImage;
                }

                // Actualizar el comprador en la base de datos
                var updateResult = await productRepository.UpdateAsync(existingProduct);

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
