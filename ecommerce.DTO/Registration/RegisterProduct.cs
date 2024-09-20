namespace ecommerce.DTO.Registration
{
    public class RegisterProduct
    {
        public long SellerId { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string? Price { get; set; }
        public string? Stock { get; set; }
        public string UrlImage { get; set; } = null!;
    }
}
