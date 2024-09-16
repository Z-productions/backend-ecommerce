using ecommerce.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.BLL.Servicios.Contrato
{
    public interface IDocumentTypeService
    {
        Task<List<DocumentTypeDto>> GetDocumentTypeAsync();
        Task<DocumentTypeDto> GetDocumentTypeName(string name);
    }
}
