using ecommerce.DTO.Common;
using ecommerce.DTO.Registration;

namespace ecommerce.BLL.Servicios.Contrato
{
    public interface ISellerService
    {
        // Crear vendedor
        Task<RegisterSellerDto> CreateSeller(RegisterSellerDto model);
        // Eliminar vendedor
        Task<bool> DeleteSeller(long sellerId);
        // Actualizar vendedor
        Task<bool> UpdateSeller(SellerDto sellerDto);
        // Traer numero de cedula de la persona
        Task<SellerWithUserDto> GetSellerWithUserInfoByDocumentNumber(string documentNumber);
    }
}

