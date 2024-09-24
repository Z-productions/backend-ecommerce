using ecommerce.DTO.Common;
using ecommerce.DTO.Registration;

namespace ecommerce.BLL.Servicios.Contrato
{
    public interface IProductService
    {
        // Crear producto
        Task<RegisterProduct> CreateProduct(RegisterProduct model);
        // Eliminar producto
        Task<bool> DeleteProduct(long productId);
        // Actualizar producto
        Task<bool> UpdateProduct(ProductDto productDto);
        // Traer producto
        Task<List<ProductDto>> GetProduct();
    }
}
