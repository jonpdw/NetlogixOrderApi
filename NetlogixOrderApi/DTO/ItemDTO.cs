using System.ComponentModel.DataAnnotations;

namespace NetlogixOrderApi.DTO
{
    public class ItemDTO
    {
        public string ItemCode { get; set; }
        [Required]
        public int? Quantity { get; set; }
        // for the [Required] attribute to work, int must be nullable so it doesn't default to 0
    }
}