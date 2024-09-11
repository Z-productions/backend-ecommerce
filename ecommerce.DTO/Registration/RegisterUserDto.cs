using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.DTO.Registration
{
    public class RegisterUserDto
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ImageUrl { get; set; }
        public UserType UserType { get; set; }  // Rol del usuario (Comprador o Vendedor)
        public RegisterBuyerDto? Buyer { get; set; } // Solo si es comprador
        public RegisterSellerDto? Seller { get; set; } // Solo si es vendedor
    }
    public enum UserType
    {
        Buyer, // Solo si es comprador
        Seller // Solo si es vendedor
    }
}
