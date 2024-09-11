using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.DTO.Common
{
    public class PaymentDto
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public float? Amount { get; set; }
        public DateTime? DatePayment { get; set; }
        public string PaymentMethod { get; set; } = null!;
    }
}
