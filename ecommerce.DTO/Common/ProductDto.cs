using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.DTO.Common
{
    public class ProductDto
    {
        public long Id { get; set; }
        public long SellerId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public float Price { get; set; }
        public int Stock { get; set; }
    }
}
