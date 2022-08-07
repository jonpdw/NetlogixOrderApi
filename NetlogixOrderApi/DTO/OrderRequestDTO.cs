
namespace NetlogixOrderApi.DTO
{
    public class OrderRequestDTO
    {
        public string OrderId { get; set; }
        public string RequestedPickupTime { get; set; }
        public PickupAddressDTO PickupAddress { get; set; }
        public DeliveryAddressDTO DeliveryAddress { get; set; }
        public ICollection<ItemDTO> Items { get; set; }
        public string PickupInstructions { get; set; }
        public string DeliveryInstructions { get; set; }
    }
}