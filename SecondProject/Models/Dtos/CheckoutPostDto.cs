using System.ComponentModel.DataAnnotations;

namespace SecondProject.Models.Dtos
{
    public class CheckoutPostDto
    {
        [Required, Range(1, Int32.MaxValue)]
        public decimal OrderTotal { get; set; }

        [Required, Range(1, 3, ErrorMessage = "ShippingMethodId must be 1 for FreeShipping, 2 for LocalShipping, or 3 for WorldWideShipping.")]

        public int ShippingMethodId { get; set; }

    }
}
