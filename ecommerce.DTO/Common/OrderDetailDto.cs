using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.DTO.Common
{
    public class OrderDetailDto
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
