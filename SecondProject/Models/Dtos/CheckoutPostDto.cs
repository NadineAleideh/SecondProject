namespace SecondProject.Models.Dtos
{
    public class CheckoutPostDto
    {
        public decimal OrderTotal { get; set; }
        public int ShippingMethodId { get; set; }
    }
}
