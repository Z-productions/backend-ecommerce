using ecommerce.DTO.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.BLL.Servicios.Contrato
{
    public interface ISeller
    {
        // Crear vendedor
        Task<RegisterSellerDto> CreateSeller(RegisterSellerDto model);

        // Actualizar vendedor
        Task<RegisterSellerDto> UpdateSeller(RegisterSellerDto model);
    }
}
