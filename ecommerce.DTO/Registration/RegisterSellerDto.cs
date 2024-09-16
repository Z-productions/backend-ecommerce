using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.DTO.Registration
{
    public class RegisterSellerDto
    {
        public long UserId { get; set; }
        public long DocumentTypeId { get; set; }
        public string DocumentNumber { get; set; } = null!;
        public string Bank { get; set; } = null!;
        public string AccountNumber { get; set; } = null!;
    }
}
