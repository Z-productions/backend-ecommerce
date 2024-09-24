using ecommerce.DTO.Common;
using ecommerce.DTO.Registration;

namespace ecommerce.BLL.Servicios.Contrato
{
    public interface IBuyerService
    {
        // Crear comprador
        Task<RegisterBuyerDto> CreateBuyer(RegisterBuyerDto model);
        // Eliminar comprador
        Task<bool> DeleteBuyer(long buyerId);
        // Actualizar comprador
        Task<bool> UpdateBuyer(BuyerDto buyerDto);
        // Traer numero de cedula de la persona
        Task<BuyerWithUserDto> GetBuyerWithUserInfoByDocumentNumber(string documentNumber);
    }
}
