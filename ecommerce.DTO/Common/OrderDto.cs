using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.DTO.Common
{
    public class OrderDto
    {
        public long Id { get; set; }
        public long BuyerId { get; set; }
        public DateOnly? DateBuyer { get; set; }
        public bool? State { get; set; }
        public List<DetailDto> Details { get; set; } = new List<DetailDto>();
        public PaymentDto? Payment { get; set; }
        public RemunerationDto? Remuneration { get; set; }
    }
}
