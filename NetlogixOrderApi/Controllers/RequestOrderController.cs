using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetlogixOrderApi.DTO;
using NetlogixOrderApi.Model;

namespace NetlogixOrderApi.Controllers
{
    // todo enable auth
    // [ApiKeyAuthAttribute]
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

        [HttpPost]
        [Route("/order")]
        public async Task<IResult> PostOrder(OrderRequestDTO orderDTO)
        {
            var order = mapper.Map<OrderRequest>(orderDTO);
            await db.Orders.AddAsync(order);
            await db.SaveChangesAsync();
            // return StatusCode(201);
            return Results.Created($"/order/{orderDTO.OrderId}", orderDTO);
        }

        [HttpGet]
        [Route("/order/{id}")]
        public async Task<OrderRequest?> GetOrder(string id)
        {
            var order = await db.Orders
            .Include(x => x.DeliveryAddress)
            .Include(x => x.Items)
            .Include(x => x.PickupAddress)
            .Where(x => x.OrderId == id).FirstOrDefaultAsync();
            return order;
        }

        [HttpGet]
        [Route("/orders")]
        public async Task<List<OrderRequestDTO>> GetOrders()
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
