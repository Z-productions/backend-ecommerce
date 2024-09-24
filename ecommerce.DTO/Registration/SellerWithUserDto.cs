using ecommerce.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.DTO.Registration
{
    public class SellerWithUserDto
    {
        public UserDto UserDto { get; set; }
        public SellerDto SellerDto { get; set; }
        public DocumentTypeDto DocumentType { get; set; }
        public SellerWithUserDto(UserDto userDto, SellerDto sellerDto, DocumentTypeDto documentType)
        {
            UserDto = userDto;
            SellerDto = sellerDto;
            DocumentType = documentType;
        }
    }
}
