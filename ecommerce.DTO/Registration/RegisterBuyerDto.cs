namespace ecommerce.DTO.Registration
{
    public class RegisterBuyerDto
    {
        public long UserId {  get; set; }   
        public long DocumentTypeId { get; set; }
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string DocumentNumber { get; set; } = null!;
    }
}
