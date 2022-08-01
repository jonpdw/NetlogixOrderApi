using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NetlogixOrderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequestOrderController : ControllerBase
    {
        IMapper mapper;
        OrderDb db;
        public RequestOrderController(IMapper _mapper, OrderDb _db)
        {
            mapper = _mapper;
            db = _db;
        }

        [HttpPost(Name = "/order")]
        public async Task<IResult> PostOrder(OrderRequestDTO orderDTO)
        {
            var order = mapper.Map<OrderRequest>(orderDTO);
            await db.Orders.AddAsync(order);
            await db.SaveChangesAsync();
            return Results.Created($"/order/{orderDTO.OrderId}", orderDTO);
        }

        [HttpGet(Name = "/order/{id}")]
        public async Task<OrderRequest?> GetOrder(int id)
        {
            return await db.Orders.FindAsync(id);
        }

        [HttpGet(Name = "/testing")]
        public async Task<List<OrderRequestDTO>> GetAllOrders()
        {
            var orders = await db.Orders
            .Include(x => x.DeliveryAddress)
            .Include(x => x.Items)
            .Include(x => x.PickupAddress)
            .ToListAsync();
            var ordersDTO = mapper.Map<List<OrderRequestDTO>>(orders);
            return ordersDTO;
        }
    }
}
