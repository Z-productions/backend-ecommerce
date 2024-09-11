using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.DTO.Common
{
    public class BuyerDto
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long DocumentTypeId { get; set; }

        public string Phone { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string DocumentNumber { get; set; } = null!;
    }
}
