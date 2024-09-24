using ecommerce.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.DTO.Registration
{
    public class BuyerWithUserDto
    {
        public UserDto UserDto { get; set; }
        public BuyerDto BuyerDto { get; set; }
        public DocumentTypeDto DocumentTypeDto { get; set; }

        public BuyerWithUserDto(UserDto userDto, BuyerDto buyerDto, DocumentTypeDto documentTypeDto)
        {
            UserDto = userDto;
            BuyerDto = buyerDto;
            DocumentTypeDto = documentTypeDto;
        }
    }
}
