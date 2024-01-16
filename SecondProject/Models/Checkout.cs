using System.ComponentModel.DataAnnotations;

namespace SecondProject.Models
{
    public class Checkout
    {

        public int Id { get; set; }

        [Required, Range(1, Int32.MaxValue)]
        public int ShippingMethodId { get; set; }

        [Required, Range(1, Int32.MaxValue)]
        public decimal OrderTotal { get; set; }

        public decimal FinalTotal { get; set; }
        public ShippingMethod shippingMethod { get; set; }

    }
}
