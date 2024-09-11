using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.DTO.Registration
{
    public class RegisterBuyerDto
    {
        public long DocumentTypeId { get; set; }
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string DocumentNumber { get; set; } = null!;
    }
}
