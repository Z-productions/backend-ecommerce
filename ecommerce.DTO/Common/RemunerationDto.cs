using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.DTO.Common
{
    public class RemunerationDto
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long SellerId { get; set; }
        public float Balance { get; set; }
    }
}
