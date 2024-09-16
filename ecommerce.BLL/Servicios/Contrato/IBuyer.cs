using ecommerce.DTO.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.BLL.Servicios.Contrato
{
    public interface IBuyer
    {
        // Crear comprador
        Task<RegisterBuyerDto> CreateBuyer(RegisterBuyerDto model);
        // Actualizar comprador
        Task<RegisterBuyerDto> UpdateBuyer(RegisterBuyerDto model);
    }
}
