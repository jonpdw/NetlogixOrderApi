using System.ComponentModel.DataAnnotations;

namespace NetlogixOrderApi.DTO
{
    public class ItemDTO
    {
        public string ItemCode { get; set; }
        [Required]
        public int? Quantity { get; set; }
    }
}