﻿namespace ecommerce.DTO.Common
{
    public class ProductDto
    {
        public long Id { get; set; }
        public long SellerId { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public float Price { get; set; }
        public int Stock { get; set; }
        public string UrlImage { get; set; } = null!;
    }
}
