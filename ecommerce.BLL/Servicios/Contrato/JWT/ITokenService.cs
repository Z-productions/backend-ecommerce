using ecommerce.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.BLL.Servicios.Contrato.JWT
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
