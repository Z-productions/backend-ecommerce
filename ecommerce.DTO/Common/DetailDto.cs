using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.DTO.Common
{
    public class DetailDto
    {
        public long Id { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }
    }
}
