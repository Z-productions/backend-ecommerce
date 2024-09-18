using ecommerce.DTO.Common;
using ecommerce.DTO.Registration;

namespace ecommerce.BLL.Servicios.Contrato
{
    public interface ISellerService
    {
        // Crear vendedor
        Task<RegisterSellerDto> CreateSeller(RegisterSellerDto model);
        // Actualizar vendedor
        Task<bool> UpdateSeller(long sellerId);
        // Actualizar comprador
        Task<bool> UpdateBuyer(SellerDto sellerDto);
    }
}

