namespace SecondProject.Models
{
    public class Checkout
    {
        public int Id { get; set; }
        public int ShippingMethodId { get; set; }
        public decimal OrderTotal { get; set; }
        public decimal FinalTotal { get; set; }
        public ShippingMethod shippingMethod { get; set; }

    }
}
