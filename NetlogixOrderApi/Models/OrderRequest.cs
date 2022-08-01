using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetlogixOrderApi.Models
{
    public class OrderRequest
    {
        [Key]
        public int OrderId { get; set; }
        public string RequestedPickupTime { get; set; }
        [ForeignKey("PickupAddressId")]
        public PickupAddress PickupAddress { get; set; }
        [ForeignKey("DeliveryAddressId")]
        public DeliveryAddress DeliveryAddress { get; set; }
        public ICollection<Item> Items { get; set; }
        public string PickupInstructions { get; set; }
        public string DeliveryInstructions { get; set; }
    }


}