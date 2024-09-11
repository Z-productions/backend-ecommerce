using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.DTO.Common
{
    public class DocumentTypeDto
    {
        public long Id { get; set; }
        public string Acronyms { get; set; } = null!;
        public string DocumentName { get; set; } = null!;
    }
}
