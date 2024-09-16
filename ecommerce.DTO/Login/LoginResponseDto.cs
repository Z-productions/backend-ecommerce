using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.DTO.Login
{
    public class LoginResponseDto
    {
        public string Login { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
