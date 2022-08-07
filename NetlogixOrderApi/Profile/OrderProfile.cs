using AutoMapper;
using NetlogixOrderApi.DTO;
using NetlogixOrderApi.Model;

namespace NetlogixOrderApi.AutoMapperProfile
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderRequest, OrderRequestDTO>().ReverseMap();
            CreateMap<Item, ItemDTO>().ReverseMap();
            CreateMap<DeliveryAddress, DeliveryAddressDTO>().ReverseMap();
            CreateMap<PickupAddress, PickupAddressDTO>().ReverseMap();
        }
    }

}
